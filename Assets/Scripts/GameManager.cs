using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour 
{
	//TODO build the game from scratch

	public bool gameStarted;
	public bool turnStarted;
	public Vector2 ballPosition;
	public Side possession;
	public bool playerTurn;
	public bool playerHasBall;
	public bool noFightNextTurn;
	public int currentMinute;
	public MatchStatistics stats;
	public Player player;
	public Team playerTeam;
	public Team enemyTeam;
	public int gameSpeed;

	public static GameManager instance;

	public event Action onGoal;
	public event Action onPlayerMove;
	public event Action onTurnStart;
	public event Action onMatchStart;
	public event Action onMatchEnd;
	public event Action onTurnEnd;
	public event Action onPlayerTurnStart;
	public event Action onPlayerTurnEnd;
	public event Action onBallMove;
	public event Action onPlayerGoal;
	public event Action onPlayerTeamGoal;
	public event Action onEnemyTeamGoal;
	public event Action onPlayerMiss;
	public event Action onPlayerTeamMiss;
	public event Action onEnemyTeamMiss;
	public event Action onComputerAttack;
	public event Action onChangePossession;
	public event Action onHalfTime;
	public event Action onPause;
	public event Action onUnpause;


	private bool initEnded;
	private string selectedMove;
	private bool paused;

	void Awake()
	{
		if(instance==null)
			instance=this;
		else if(instance!=this)
			Destroy(this);

		onMatchStart+=InitVariables;
		onPlayerTurnEnd+=EndTurn;
		onPlayerGoal+=onPlayerTeamGoal;
		onPlayerMiss+=onPlayerTeamMiss;
	}

	void Start () 
	{
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void InitVariables()
	{
		gameSpeed=20;
		gameStarted=true;
		playerTurn=false;
		currentMinute=1;
		turnStarted=false;

		noFightNextTurn=false;
		SetBallPosition(new Vector2(0,0));
		stats = new MatchStatistics(new Team("PlayerTeam", stats.playerTeam.defence, stats.playerTeam.midfield, stats.playerTeam.attack), new Team("EnemyTeam", stats.enemyTeam.defence, stats.enemyTeam.midfield, stats.enemyTeam.attack));
		playerTeam=stats.playerTeam;
		enemyTeam=stats.enemyTeam;
	}

	public void StartTheMatch()
	{
		if(gameStarted)
			return;
		gameStarted=true;
		if(onMatchStart!=null)
			onMatchStart();

	}

	public void EndTheMatch()
	{
		if(!gameStarted)
			return;
		gameStarted=false;
		playerTurn=false;
		playerHasBall=false;
		if(onMatchEnd!=null)
			onMatchEnd();
	}
		

	void StartTurn()
	{
		if(onTurnStart!=null)
			onTurnStart();
		turnStarted=true;

		if(CalculationsManager.CanPlayerStart())
		{
			playerHasBall=true;
			BeginPlayerTurn();
		}
		else if(CalculationsManager.CanPlayerTackle()&&!noFightNextTurn)
		{
			bool wasTackleSuccessful=player.Tackle();
			if(wasTackleSuccessful)
			{
				ChangeBallPossession(Side.PLAYER);
				playerHasBall=true;
				BeginPlayerTurn();
			}
			else
			{
				playerHasBall=false;
				BeginComputerTurn();
			}
		}
		else
			BeginComputerTurn();
	}

	void Update () 
	{

		if(gameStarted&&!turnStarted&&!paused)
			StartTurn();
	}

	public bool HasTheGameStarted()
	{
		return gameStarted;
	}

	void BeginPlayerTurn()
	{
		playerTurn=true;
		if(onPlayerTurnStart!=null)
			onPlayerTurnStart();

	}

	public void EndPlayerTurn()
	{
		playerTurn=false;
		if(onPlayerTurnEnd!=null)
			onPlayerTurnEnd();
		
	}

	void HalfTime()
	{
		SetBallPosition(Vector2.zero);
		ChangeBallPossession(Side.ENEMY);
		Pause();

		if(onHalfTime!=null)
			onHalfTime();
	}

	void EndTurn()
	{
		turnStarted=false;
		currentMinute++;
		if(currentMinute==46)
			HalfTime();

		player.ReduceEnergyBy(1);
		if(onTurnEnd!=null)
			onTurnEnd();
		if(currentMinute>90)
			EndTheMatch();
	}

	public void Pause()
	{
		paused=true;

		if(onPause!=null)
			onPause();
	}

	public void Unpause()
	{
		paused=false;

		if(onUnpause!=null)
			onUnpause();
	}

	void BeginComputerTurn()
	{
		StartCoroutine(ActualComputerTurn());
	}

	IEnumerator ActualComputerTurn()
	{
		yield return new WaitForSeconds(4f/gameSpeed);

		if(!noFightNextTurn)
			FightForBall();
		else
			noFightNextTurn=false;

		if (CalculationsManager.CanComputerShootOnPenaltyArea())
			ComputerShoot();
		else
			ComputerAttack();
	}
		

	void ComputerAttack()
	{
		Vector2 destination=CalculationsManager.GetRandomAttackingPosition(ballPosition, possession);
		SetBallPosition(destination);
		if(onComputerAttack!=null)
			onComputerAttack();

		EndComputerTurn();
	}

	void EndComputerTurn()
	{
		EndTurn();
	}

	void ComputerShoot()
	{
		if(CalculationsManager.IsComputerShootSuccessful(possession))
		{
			Goal(false, possession);
		}
		else
		{
			Miss(false, possession);
		}
		ChangeBallPossession(CalculationsManager.OtherSide(possession));
		EndComputerTurn();
	}

	public void SetSelectedMove(string move)
	{
		selectedMove=move;
	}

	public string GetSelectedMove()
	{
		return selectedMove;
	}

	public void MakeSelectedMove(Vector2 destination)
	{
		MakeMove(selectedMove, destination);
	}

	public void MakeMove(string name, Vector2 destination)
	{
		if(name.Equals("Pass"))
			player.Pass(destination);
		else if(name.Equals("Cross"))
			player.Cross(destination);
		else if(name.Equals("Shoot"))
			player.FinishShoot();
		else if(name.Equals("Dribble"))
			player.Dribble(destination);
		else if(name.Equals("LongShot"))
			player.LongShot();
	}

	public void SetBallPosition(Vector2 destination)
	{
		ballPosition=destination;

		if(onBallMove!=null)
			onBallMove();
	}

	public void ChangeBallPossession(Side s)
	{
		possession=s;
		if(onChangePossession!=null)
			onChangePossession();
	}
		
	void FightForBall()
	{
		int playerScore = CalculationsManager.GetFormationPointsInPosition(ballPosition, Side.PLAYER)+CalculationsManager.RollTheDice();
		int enemyScore = CalculationsManager.GetFormationPointsInPosition(ballPosition, Side.ENEMY)+CalculationsManager.RollTheDice();

		if (playerScore==enemyScore)
			EndComputerTurn();
		else
			ChangeBallPossession(playerScore > enemyScore ? Side.PLAYER : Side.ENEMY);

	}

	public Vector2 GetPlayerPosition()
	{
		return player.position;
	}

	public static GameManager GetInstanceOf()
	{
		return instance;
	}

	public void Goal(bool playerGoal, Side side)
	{
		stats.AddGoal(side, playerGoal);
		if(playerGoal)
		{
			if(onPlayerGoal!=null)
				onPlayerGoal();
		}
		else if(side==Side.PLAYER)
		{
			if(onPlayerTeamGoal!=null)
				onPlayerTeamGoal();
		}
		else
		{
			if(onEnemyTeamGoal!=null)
				onEnemyTeamGoal();
		}
		SetBallPosition(Vector2.zero);
	}

	public void Miss(bool playerMiss, Side side)
	{
		if(playerMiss)
		{
			if(onPlayerMiss!=null)
				onPlayerMiss();
			SetBallPosition(Vector2.right);
		}
		else if(side==Side.PLAYER)
		{
			if(onPlayerTeamMiss!=null)
				onPlayerTeamMiss();
			SetBallPosition(Vector2.right);
		}
		else
		{
			if(onEnemyTeamMiss!=null)
				onEnemyTeamMiss();
			SetBallPosition(Vector2.left);
		}
	}

	public void PlayerMoved()
	{
		if(onPlayerMove!=null)
			onPlayerMove();
	}

	public bool IsGamePaused()
	{
		return paused;
	}
}
