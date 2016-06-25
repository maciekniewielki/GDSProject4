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

	public static GameManager instance;

	public event Action onGoal;
	public event Action onPlayerMove;
	public event Action onTurnStart;
	public event Action onMatchStart;
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


	private bool initEnded;
	private string selectedMove;

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
		onPlayerTeamGoal+=onGoal;
		onEnemyTeamGoal+=onGoal;
		onGoal+=OnGoal;
	}

	void Start () 
	{
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void InitVariables()
	{
		gameStarted=true;
		playerTurn=false;
		currentMinute=1;
		turnStarted=false;

		noFightNextTurn=false;
		//gameSpeed=(int)GameObject.Find("Speed").GetComponent<Slider>().value;
		SetBallPosition(new Vector2(0,0));
		stats = new MatchStatistics(new Team("PlayerTeam", 3, 3, 3), new Team("EnemyTeam", 3, 3, 3));
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

	void OnGoal()
	{
		SetBallPosition(Vector2.zero);
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
		else if(CalculationsManager.CanPlayerTackle())
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
		if(gameStarted&&!turnStarted)
			StartTurn();
		if(Input.GetKeyDown(KeyCode.Space))
			EndPlayerTurn();
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

	void EndTurn()
	{
		turnStarted=false;
		currentMinute++;
		if(onTurnEnd!=null)
			onTurnEnd();
	}

	void BeginComputerTurn()
	{
		StartCoroutine(ActualComputerTurn());
	}

	IEnumerator ActualComputerTurn()
	{
		yield return new WaitForSeconds(1);

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
		//BeginPlayerTurn();
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
	}

	public void Miss(bool playerMiss, Side side)
	{
		if(playerMiss)
		{
			if(onPlayerMiss!=null)
				onPlayerMiss();
		}
		else if(side==Side.PLAYER)
		{
			if(onPlayerTeamMiss!=null)
				onPlayerTeamMiss();
		}
		else
		{
			if(onEnemyTeamMiss!=null)
				onEnemyTeamMiss();
		}
	}

	public void PlayerMoved()
	{
		if(onPlayerMove!=null)
			onPlayerMove();
	}
}
