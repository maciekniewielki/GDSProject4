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

	public static GameManager instance;
	public static MatchStatistics stats;
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


	private Player player;
	private bool initEnded;
	private string selectedMove;

	void Awake()
	{
		onMatchStart+=InitVariables;
		onPlayerTurnEnd+=EndTurn;
	}

	void Start () 
	{
		if(instance==null)
			instance=this;
		else if(instance!=this)
			Destroy(this);

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
		stats = new MatchStatistics();

	}

	public void StartTheMatch()
	{
		if(gameStarted)
			return;
		if(onMatchStart!=null)
			onMatchStart();

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
	
	// Update is called once per frame
	void Update () 
	{
	
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
		if(onTurnEnd!=null)
			onTurnEnd();
	}

	void BeginComputerTurn()
	{
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
		
	}

	void EndComputerTurn()
	{
		
	}

	void ComputerShoot()
	{
		if(CalculationsManager.IsComputerShootSuccessful())
		{
			if(possession==Side.PLAYER&&onPlayerTeamGoal!=null)
				onPlayerTeamGoal();
			else if(possession==Side.ENEMY&&onEnemyTeamGoal!=null)
				onEnemyTeamGoal();
		}
		else
		{
			if(possession==Side.PLAYER&&onPlayerTeamMiss!=null)
				onPlayerTeamMiss();
			else if(possession==Side.ENEMY&&onEnemyTeamMiss!=null)
				onEnemyTeamMiss();
		}

	}

	public void SetSelectedMove(string move)
	{
		selectedMove=move;
	}

	public void GetSelectedMove()
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
}
