﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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
	public DesignToolManager logs;
	public RestartAction nextAction;

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
	public event Action onCornerBegin;
	public event Action onCornerEnd;


	private bool initEnded;
	private string selectedMove;
	private bool paused;
	private bool playerRemoved;
	public bool playerRestartMoveRemaining;

	//Debug
	public int ReceiveChanceWhen1Heart=10;
	public int ReceiveChanceWhen2Hearts=40;
	public int ReceiveChanceWhen3Hearts=70;

	void Awake()
	{
		gameSpeed=20;
		if(instance==null)
			instance=this;
		else if(instance!=this)
			Destroy(this);

		onMatchStart+=InitVariables;
		onPlayerTurnEnd+=EndTurn;
		onPlayerGoal+=onPlayerTeamGoal;
		onPlayerMiss+=onPlayerTeamMiss;
		player.onEnergyDeplete+=RemovePlayer;
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
		playerRemoved=false;

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

		Invoke("LoadEndMatchStatistics", 3);
	}
		
	void LoadEndMatchStatistics()
	{
		SceneManager.LoadScene("matchStatistics");
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

		if(gameStarted&&!turnStarted&&!paused&&!playerRestartMoveRemaining)
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
		if(playerRemoved)
			EndPlayerTurn();

	}

	public void EndPlayerTurn()
	{
		playerTurn=false;
		playerHasBall=false;
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

		player.ReduceEnergy();
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
		bool isDraw=false;
		if(!noFightNextTurn)
			isDraw=FightForBall();
		else
			noFightNextTurn=false;

		if(!isDraw)
		{
			if (CalculationsManager.CanComputerShootOnPenaltyArea())
				ComputerShoot();
			else
				ComputerAttack();
		}
			
		yield return new WaitForSeconds(4f/gameSpeed);
		EndComputerTurn();
	}
		

	void ComputerAttack()
	{
		Vector2 destination=CalculationsManager.GetRandomAttackingPosition(ballPosition, possession);
		SetBallPosition(destination);
		if(onComputerAttack!=null)
			onComputerAttack();
	}

	void EndComputerTurn()
	{
		EndTurn();
	}

	public void ComputerShoot()
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
		else if(name.Equals("Corner"))
			player.Corner();
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
		
	bool FightForBall()
	{
		int playerScore = CalculationsManager.GetFormationPointsInPosition(ballPosition, Side.PLAYER)+CalculationsManager.RollTheDice();
		int enemyScore = CalculationsManager.GetFormationPointsInPosition(ballPosition, Side.ENEMY)+CalculationsManager.RollTheDice();

		if (playerScore==enemyScore)
			return true;
		else
		{
			ChangeBallPossession(playerScore > enemyScore ? Side.PLAYER : Side.ENEMY);
			return false;
		}

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

	public bool IsPlayerWaitingForRestart()
	{
		return playerRestartMoveRemaining;
	}

	public void PreparePlayerForRestartMove()
	{
		if(!nextAction.isPlayerPerforming)
			return;
		playerRestartMoveRemaining=true;
		if(nextAction.type==RestartActionType.CORNER)
		{
			if(onCornerBegin!=null)
				onCornerBegin();
		}
	}

	public void EndPlayerRestartMove()
	{
		playerRestartMoveRemaining=false;
		if(onCornerEnd!=null)
			onCornerEnd();
	}

	void RemovePlayer()
	{
		playerRemoved=true;
		player.position=Vector2.right*2;
	}
}