using UnityEngine;
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
	public bool playerRestartMoveRemaining;
	public bool CPURestartMoveRemaining;

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
	public event Action onOutBegin;
	public event Action onFreeKickBegin;


	private bool initEnded;
	private string selectedMove;
	private bool paused;
	private bool playerRemoved;



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
		playerRestartMoveRemaining=false;
		CPURestartMoveRemaining=false;

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

		if(gameStarted&&!turnStarted&&!paused&&!playerRestartMoveRemaining&&!CPURestartMoveRemaining)
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
		else if(name.Equals("Move"))
			player.MoveYourselfAction(destination);
		else if(name.Equals("Out"))
			player.Out();
		else if(name.Equals("LongOut"))
			player.LongOut(destination);
		else if(name.Equals("Head")&&nextAction.source!=Vector2.right)
			player.Head(destination);
		else if(name.Equals("Head")&&nextAction.source==Vector2.right)
			player.FinishHead();
		else if(name.Equals("Freekick"))
			player.FreeKick();
		
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

	public void PrepareForRestartMove()
	{
		if(!nextAction.isPlayerPerforming)
		{
			if(nextAction.type==RestartActionType.CORNER)
			{
				SetBallPosition(nextAction.source);
				Invoke("PreComputerCorner", 4f/gameSpeed);
			}
			else if(nextAction.type==RestartActionType.OUT)
			{
				FightForBall();
			}
			else if(nextAction.type==RestartActionType.FREEKICK)
			{
				Invoke("ComputerFreeKick", 4f/gameSpeed);
			}
			CPURestartMoveRemaining=true;
		}
		else
			playerRestartMoveRemaining=true;
		if(nextAction.type==RestartActionType.CORNER)
		{
			if(onCornerBegin!=null)
				onCornerBegin();
		}
		else if(nextAction.type==RestartActionType.OUT)
		{
			if(onOutBegin!=null)
				onOutBegin();
		}
		else if(nextAction.type==RestartActionType.FREEKICK)
		{
			if(onFreeKickBegin!=null)
				onFreeKickBegin();
		}
	}

	void PreComputerCorner()
	{
		SetBallPosition(Vector2.right);
		Invoke("ComputerCorner", 4f/gameSpeed);
	}

	void ComputerCorner()
	{
		if(CalculationsManager.IsComputerCornerSuccessful(possession))
		{
			player.actionList.cornerCPU.subActions[1].MakeAction();
		}
		else
		{
			player.actionList.cornerCPU.subActions[0].MakeAction();
		}
		EndCPURestartMove();
	}

	void ComputerFreeKick()
	{
		if(CalculationsManager.IsComputerShootSuccessful(possession))
		{
			player.actionList.FreeKickCPU.subActions[1].MakeAction();
		}
		else
		{
			player.actionList.FreeKickCPU.subActions[0].MakeAction();
		}
		EndCPURestartMove();
	}

	void EndCPURestartMove()
	{
		CPURestartMoveRemaining=false;
		if(onCornerEnd!=null)
			onCornerEnd();
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

	public void Card(bool isPlayer, Side side, Vector2 position, string color)
	{
		if(isPlayer)
			if(color.Equals("yellow"))
				player.GetYellowCard();
			else
				player.GetRedCard();
		else
		{
			if(color.Equals("red"))
			{
				if(position.x==1&&side==Side.PLAYER)
					ReduceFormationPoints("attack", side);
				else if(position.x==0&&side==Side.PLAYER)
					ReduceFormationPoints("midfield", side);
				else if(position.x==-1&&side==Side.PLAYER)
					ReduceFormationPoints("defence", side);
				else if(position.x==1&&side==Side.ENEMY)
					ReduceFormationPoints("defence", side);
				else if(position.x==0&&side==Side.ENEMY)
					ReduceFormationPoints("midfield", side);
				else if(position.x==-1&&side==Side.ENEMY)
					ReduceFormationPoints("attack", side);
			}
			else if(color.Equals("yellow"))
			{
				if(position.x==1&&side==Side.PLAYER)
					GiveYellowCardToFormation("attack", side);
				else if(position.x==0&&side==Side.PLAYER)
					GiveYellowCardToFormation("midfield", side);
				else if(position.x==-1&&side==Side.PLAYER)
					GiveYellowCardToFormation("defence", side);
				else if(position.x==1&&side==Side.ENEMY)
					GiveYellowCardToFormation("defence", side);
				else if(position.x==0&&side==Side.ENEMY)
					GiveYellowCardToFormation("midfield", side);
				else if(position.x==-1&&side==Side.ENEMY)
					GiveYellowCardToFormation("attack", side);
			}
		}
	}


	public void GiveYellowCardToFormation(string formation, Side side)
	{
		if(formation.Equals("attack")&&side==Side.PLAYER)
		{
			if(!CalculationsManager.IsYellowCardRed(playerTeam.attackYellowCards))
				playerTeam.attackYellowCards++;
			else
			{
				ReduceFormationPoints("attack", side);
				playerTeam.attackYellowCards--;
			}
		}
		else if(formation.Equals("midfield")&&side==Side.PLAYER)
		{
			if(!CalculationsManager.IsYellowCardRed(playerTeam.midfieldYellowCards))
				playerTeam.midfieldYellowCards++;
			else
			{
				ReduceFormationPoints("midfield", side);
				playerTeam.midfieldYellowCards--;
			}
		}
		else if(formation.Equals("defence")&&side==Side.PLAYER)
		{
			if(!CalculationsManager.IsYellowCardRed(playerTeam.defenceYellowCards))
				playerTeam.defence++;
			else
			{
				ReduceFormationPoints("defence", side);
				playerTeam.defenceYellowCards--;
			}
		}
		else if(formation.Equals("attack")&&side==Side.ENEMY)
		{
			if(!CalculationsManager.IsYellowCardRed(enemyTeam.attackYellowCards))
				enemyTeam.attackYellowCards++;
			else
			{
				ReduceFormationPoints("attack", side);
				enemyTeam.attackYellowCards--;
			}
		}
		else if(formation.Equals("midfield")&&side==Side.ENEMY)
		{
			if(!CalculationsManager.IsYellowCardRed(enemyTeam.midfieldYellowCards))
				enemyTeam.midfieldYellowCards++;
			else
			{
				ReduceFormationPoints("midfield", side);
				enemyTeam.midfieldYellowCards--;
			}
		}
		else if(formation.Equals("defence")&&side==Side.ENEMY)
		{
			if(!CalculationsManager.IsYellowCardRed(enemyTeam.defenceYellowCards))
				enemyTeam.defenceYellowCards++;
			else
			{
				ReduceFormationPoints("defence", side);
				enemyTeam.defenceYellowCards--;
			}
		}
	}

	public void ReduceFormationPoints(string formation, Side side)
	{
		if(formation.Equals("attack")&&side==Side.PLAYER)
		{
			if(playerTeam.attack>=0)
				playerTeam.attack--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(playerTeam), side);
		}
		else if(formation.Equals("midfield")&&side==Side.PLAYER)
		{
			if(playerTeam.midfield>=0)
				playerTeam.midfield--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(playerTeam), side);
		}
		else if(formation.Equals("defence")&&side==Side.PLAYER)
		{
			if(playerTeam.defence>=0)
				playerTeam.defence--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(playerTeam), side);
		}
		else if(formation.Equals("attack")&&side==Side.ENEMY)
		{
			if(playerTeam.attack>=0)
				playerTeam.attack--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(enemyTeam), side);
		}
		else if(formation.Equals("midfield")&&side==Side.ENEMY)
		{
			if(playerTeam.midfield>=0)
				playerTeam.midfield--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(enemyTeam), side);
		}
		else if(formation.Equals("defence")&&side==Side.ENEMY)
		{
			if(playerTeam.defence>=0)
				playerTeam.defence--;
			else
				ReduceFormationPoints(CalculationsManager.GetFormationWithMostPoints(enemyTeam), side);
		}
			
	}
}
	