using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PitchManager : MonoBehaviour 
{

    // Use this for initialization
    public enum Position
    {
        LEFT,RIGHT,MIDDLE,DEFENCE,ATTACK,MIDFIELD
    }

    public enum Side
    {
        PLAYER, ENEMY
    }
    [System.Serializable]
    public class Board
    {
        public Vector2 currentBallPosition { get; set; }
		public Vector2 currentPlayerPosition { get; set; }
        public Side currentBallPossession { get; set; }
        public int numberOfTurns {get; set;}
        public int playerTeamGoals { get; set; }
        public int enemyTeamGoals { get; set; }
        public int playerTeamShots { get; set; }
        public int enemyTeamShots { get; set; }
    }
		

    public GameObject[] fields;
    public Board board;
    public GameObject ball;
    public Text playerScore;
    public Text enemyScore;
    public Text[] logs;
    public Team playerTeam;
    public Team enemyTeam;
	public Vector2 moveDestination;
	public string selectedMove;
	public bool waitingForPlayerInput;
	public bool paused;
	public bool gameEnded;
	public bool gameStarted;
	public bool secondHalfStarted;

    private MatchStatistics stats;
	private ButtonManager buttonManager;
	private bool isTurnInProgress;
	private bool dontFightNextTurn;
	private Text timer;
	//Debug
	private int playerPassing;
	private int playerTackling;
	private int gameSpeed;
	private int playerFinishing;
	private int playerCrossing;

	void Awake()
	{
		timer=GameObject.Find("Time").GetComponent<Text>();
		buttonManager=GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
	}

	void Start ()
    {
        playerTeam = new Team("Middlebrough", 1, 1, 1);
        enemyTeam=new Team("Newcastle United", 1, 1, 1);
        board = new Board();
        InitBoard();
		SetPlayerPosition(new Vector2(0,-1));
        UpdateScore();
	}

	
	// Update is called once per frame
	void Update ()
    {
		if(waitingForPlayerInput||paused)
		{
			return;
		}

		if(gameEnded||isTurnInProgress||!gameStarted)
			return;
		else
			NewTurn();
    }

	public void StartTheMatch()
	{
		InitBoard();
		gameStarted=true;
	}

    void UpdateStatistics(string phase="")
    {
        int val;
        if (stats.fieldBallCount.TryGetValue(board.currentBallPosition, out val))
            stats.fieldBallCount[board.currentBallPosition] = val+1;
        else
            stats.fieldBallCount.Add(board.currentBallPosition, 1);

        if(phase=="end")
        {
            stats.playerTeamShots = board.playerTeamShots;
            stats.playerTeamGoals = board.playerTeamGoals;
            stats.enemyTeamShots = board.enemyTeamShots;
            stats.enemyTeamGoals = board.enemyTeamGoals;
        }
    }
    void InitBoard()
    {
		dontFightNextTurn=false;
		gameSpeed=(int)GameObject.Find("Speed").GetComponent<Slider>().value;
		waitingForPlayerInput=false;
		gameStarted=false;
		paused=false;
		isTurnInProgress=false;
		gameEnded=false;
		secondHalfStarted=false;
        logs[0].text = "Logs:\n";
        logs[1].text = "\n";
        logs[2].text = "\n";
		logs[3].text = "\n";
        board.numberOfTurns = 0;
        board.playerTeamGoals = 0;
        board.enemyTeamGoals = 0;
        board.playerTeamShots = 0;
        board.enemyTeamShots = 0;
		SetCurrentBallPosition(new Vector2(0,0));
        stats = new MatchStatistics();


		buttonManager.InitButtons();
		UnHighlightEverything();
		HighlightSpecific(board.currentPlayerPosition, "Player");
    }

    Vector2 GetRandomAttackingPosition()
    {
		/*
        Vector2 pos = board.currentBallPosition;
        pos.x *= board.currentBallPossession == Side.PLAYER ? 1 : -1;
        if (pos.x == 1)
            pos.y = 0;
        else
        {
            pos.x++;
            if (pos.y == 1)
                pos.y = Random.Range(0, 2) == 1 ? 1 : 0;
            else if (pos.y == -1)
                pos.y = Random.Range(0, 2) == 1 ? -1 : 0;
            else
                pos.y = GetRandPos();
        }
        pos.x *= board.currentBallPossession == Side.PLAYER ? 1 : -1;
        return pos;
        */
		Vector2 pos=board.currentBallPosition;
		pos.x *= board.currentBallPossession == Side.PLAYER ? 1 : -1;

		if (pos.x== 1)
			return new Vector2(board.currentBallPosition.x, 0);

		Vector2[] passingPositions=GetPassingPositions();
		

		while(true)
		{
			int randIndex=Random.Range(0,passingPositions.Length);
			if(passingPositions[randIndex].x>board.currentBallPosition.x&&board.currentBallPossession==Side.PLAYER)
				return passingPositions[randIndex];
			else if(passingPositions[randIndex].x<board.currentBallPosition.x&&board.currentBallPossession==Side.ENEMY)
				return passingPositions[randIndex];
			
		}

    }

	public Vector2[] GetPositions(string which)
	{
		if(which.Equals("Pass"))
			return GetPassingPositions();
		else
			return GetCrossingPositions();
	}

	public Vector2[] GetCrossingPositions()
	{
		return new Vector2[]{new Vector2(1,0)};
	}

	void IncrementTurnCounter()
	{
		board.numberOfTurns++;
		if(board.numberOfTurns==46&&!secondHalfStarted)
		{
			HalfTime();
			secondHalfStarted=true;
		}
	}

	public Vector2[] GetPassingPositions()
	{
		Vector2 pos = board.currentBallPosition;
		Vector2[] positions;

		if(pos.x==-1 && pos.y==1)
			positions=new Vector2[]{new Vector2(0, 1), new Vector2(0, 0), new Vector2(-1, 0)};
		else if(pos.x==0 && pos.y==1)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, 1)};
		else if(pos.x==1 & pos.y==1)
			positions=new Vector2[]{new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0)};
		else if(pos.x==-1 && pos.y==0)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 0), new Vector2(-1, -1)};
		else if(pos.x==0 && pos.y==0)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)};
		else if(pos.x==1 && pos.y==0)
			positions=new Vector2[]{new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, -1)};
		else if(pos.x==-1 && pos.y==-1)
			positions=new Vector2[]{new Vector2(-1, 0), new Vector2(0, 0), new Vector2(0, -1)};
		else if(pos.x==0 && pos.y==-1)
			positions=new Vector2[]{new Vector2(-1, -1), new Vector2(0, 0), new Vector2(1, -1)};
		else
			positions=new Vector2[]{new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, -1)};

		return positions;
	}

	public Vector2[] GetAttackingPositions()
	{
		Vector2 pos = board.currentPlayerPosition;
		Vector2[] positions;
		if (pos.x == 1)
			return null;
		else
		{
			pos.x++;
			if (pos.y == 1)
				positions=new Vector2[]{new Vector2(pos.x, pos.y), new Vector2(pos.x, 0)};
			else if (pos.y == -1)
				positions=new Vector2[]{new Vector2(pos.x, pos.y), new Vector2(pos.x, 0)};
			else
				positions=new Vector2[]{new Vector2(pos.x, 1), new Vector2(pos.x, 0), new Vector2(pos.x, -1)};
		}
		return positions;
	}

    void AddText(string what)
    {
		if(board.numberOfTurns>90)
		{
			logs[3].text = logs[3].text + what + "\n";
			return;
		}

        if (board.numberOfTurns < 23)
			logs[0].text = logs[0].text + board.numberOfTurns+"':"+ what + "\n";
        else if(board.numberOfTurns < 46)
			logs[1].text = logs[1].text + board.numberOfTurns+"':" + what + "\n";
		else if(board.numberOfTurns<69)
			logs[2].text = logs[2].text + board.numberOfTurns+"':" + what + "\n";
		else
			logs[3].text = logs[3].text + board.numberOfTurns+"':" + what + "\n";
    }

    void ChangePossession()
    {
        board.currentBallPossession = board.currentBallPossession==Side.PLAYER ? Side.ENEMY : Side.PLAYER;
        AddText((board.currentBallPossession == Side.PLAYER ? "Player" : "Enemy") + " team has taken the ball");
    }

    void ChangePossession(Side side)
    {
        board.currentBallPossession = side;
        AddText((board.currentBallPossession == Side.PLAYER ? "Player" : "Enemy") + " team has taken the ball");
    }

	void ChangeCurrentMove(string s)
	{
		selectedMove=s;
	}

	public string GetCurrentMove()
	{
		return selectedMove;
	}

	public void HighlightSelected(Vector2[] which)
	{
		if (which!=null)
			foreach(Vector2 w in which)
				HighlightField(w);
	}

	IEnumerator PlayerTurn()
	{
		waitingForPlayerInput=true;
		buttonManager.SetCurrentlyAvailable();

		if (board.currentPlayerPosition==board.currentBallPosition&&board.currentBallPosition==new Vector2(1,0))
			EnableFinish();

		yield return new WaitUntil(()=>waitingForPlayerInput==false);
		UnHighlightEverything();
		isTurnInProgress=false;

	}

	void EnableFinish()
	{
		buttonManager.SetInteractable("shootButton", true);
	}

	IEnumerator ComputerTurn()
	{
		buttonManager.SetCurrentlyAvailable();
		if(!dontFightNextTurn)
			FightForBall();
		else
			dontFightNextTurn=false;
		
		if (board.currentBallPosition == new Vector2(1, 0) && board.currentBallPossession == Side.PLAYER || board.currentBallPosition == new Vector2(-1, 0) && board.currentBallPossession == Side.ENEMY)
			Shoot();
		else
			SetCurrentBallPosition(GetRandomAttackingPosition());


		yield return new WaitForSeconds(4f/gameSpeed);
		isTurnInProgress=false;

	}

	public void FinishShoot()
	{
		int percent=playerFinishing*5;
		board.playerTeamShots++;
		ChangePossession();
		if(Random.Range(1,101)<=percent)
		{
			board.playerTeamGoals++;
			AddText("You shoot... Goal!");
			SetCurrentBallPosition(0,0);
		}
		else
		{
			AddText("You shoot... Miss.");
			dontFightNextTurn=true;
		}

		waitingForPlayerInput=false;
	}

	bool IsPlayerStandingOnBall()
	{
		if(board.currentBallPosition==board.currentPlayerPosition)
			return true;
		else return false;
	}

	void NewTurn()
    {
		isTurnInProgress=true;
		UpdateTime();
        UpdateStatistics();

		if(IsPlayerStandingOnBall()&&!dontFightNextTurn)
		{
			if(board.currentBallPossession==Side.ENEMY)
			{
				bool result=Tackle();
				if(!result)
					StartCoroutine(ComputerTurn());
				else
					StartCoroutine(PlayerTurn());
			}
			else
				StartCoroutine(PlayerTurn());
		}
		else
			StartCoroutine(ComputerTurn());


        UpdateScore();
		IncrementTurnCounter();

       
		if(board.numberOfTurns>90)
		{
	        UpdateStatistics("end");
	        AddText(stats.ToString());
			gameEnded=true;
			gameStarted=false;
			buttonManager.SetButtonText("startButton", "Play Match");
			buttonManager.SetInteractable("startButton", true);
		}
    }

	void HalfTime()
	{
		paused=true;
		buttonManager.SetButtonText("startButton", "2nd half");
		buttonManager.SetInteractable("startButton", true);
		SetCurrentBallPosition(new Vector2(0,0));

	}

	public void Unpause()
	{
		paused=false;
	}

    int RollTheDice()
    {
        return Random.Range(1, 6);
    }

    void FightForBall()
    {
        int playerScore = 0;
        int enemyScore = 0;
        
        if (board.currentBallPosition.x==-1)
        {
            playerScore += playerTeam.defence + RollTheDice();
            enemyScore += enemyTeam.attack + RollTheDice();
        }
        else if (board.currentBallPosition.x == 0)
        {
            playerScore += playerTeam.midfield + RollTheDice();
            enemyScore += enemyTeam.midfield + RollTheDice();
        }
        else if (board.currentBallPosition.x == 1)
        {
            playerScore += playerTeam.attack + RollTheDice();
            enemyScore += enemyTeam.defence + RollTheDice();
        }

        //Debug.Log("playerScore=" + playerScore + "enemyScore=" + enemyScore);

        if (playerScore==enemyScore)
        {
			IncrementTurnCounter();
            FightForBall();
        }
        else
            ChangePossession(playerScore > enemyScore ? Side.PLAYER : Side.ENEMY);
            
    }

    void UpdateScore()
    {
        playerScore.text = board.playerTeamGoals.ToString();
        enemyScore.text = board.enemyTeamGoals.ToString();
    }


    string SideToString(Side s)
    {
        return s == Side.PLAYER ? "Player" : "Enemy";
    }

    void Shoot()
    {
        if(Random.Range(0,5)==1)
        {
            AddText("Goal for the " + SideToString(board.currentBallPossession)+" team");
            if (board.currentBallPossession == Side.PLAYER)
            {
                board.playerTeamGoals++;
                board.playerTeamShots++;
            }
                
            else
            {
                board.enemyTeamGoals++;
                board.enemyTeamShots++;
            }
                

            SetCurrentBallPosition(new Vector2(0, 0));
        }
        else
        {
            if (board.currentBallPossession == Side.PLAYER)
                board.playerTeamShots++;
            else
                board.enemyTeamShots++;

            AddText("Miss");
            ChangePossession();
        }

        
    }

    int GetRandPos()
    {
        return Random.Range(-1, 2);
    }

    int Flatten(Vector2 w)
    {
        return (int)((-w.y+1)*3+w.x+1);
    }

    void SetBallGraphicalPosition(Vector2 number)
    {
        int index = Flatten(number);
        ball.transform.position = fields[index].transform.position;
    }

	void HighlightField(Vector2 which)
	{
		int index=Flatten(which);
		fields[index].GetComponent<Field>().Highlight();
		
	}

	void UnHighlightField(Vector2 which)
	{
		int index=Flatten(which);
		fields[index].GetComponent<Field>().UnHighlight();
	}

	void HighlightSpecific(Vector2 which, string what)
	{
		int index=Flatten(which);
		fields[index].GetComponent<Field>().Highlight(what);
	}

	void UnHighlightEverything()
	{
		foreach(GameObject g in fields)
			g.GetComponent<Field>().UnHighlight();
	}

	public void Refresh()
	{
		UnHighlightEverything();
		HighlightSpecific(board.currentPlayerPosition, "Player");
	}

    void SetCurrentBallPosition(Position pos1, Position pos2)
    {
        int x = 0;
        int y = 0;
        if (pos1 == Position.LEFT||pos2==Position.LEFT)
            y++;
        if (pos1 == Position.RIGHT || pos2 == Position.RIGHT)
            y--;
        if (pos1 == Position.DEFENCE || pos2 == Position.DEFENCE)
            x--;
        if (pos1 == Position.ATTACK || pos2 == Position.ATTACK)
            x++;

        SetCurrentBallPosition(x, y);
    }

    void SetCurrentBallPosition(int x, int y)
    {
        SetCurrentBallPosition(new Vector2(x, y));
    }
    void SetCurrentBallPosition(Vector2 pos)
    {
        SetBallGraphicalPosition(pos);
        board.currentBallPosition = pos;
    }

	public void SetPlayerPosition(Vector2 pos)
	{
		board.currentPlayerPosition=pos;
		Refresh();
	}
    public void SetPlayerAttack(float attack)
    {
        playerTeam.attack = (int)attack;
    }
    public void SetPlayerMidfield(float midfield)
    {
        playerTeam.midfield = (int)midfield;
    }
    public void SetPlayerDefence(float defence)
    {
        playerTeam.defence = (int)defence;
    }
    public void SetEnemyAttack(float attack)
    {
        enemyTeam.attack = (int)attack;
    }
    public void SetEnemyMidfield(float midfield)
    {
        enemyTeam.midfield = (int)midfield;
    }
    public void SetEnemyDefence(float defence)
    {
        enemyTeam.defence = (int)defence;
    }
	public void SetMoveDestination(Vector2 where)
	{
		moveDestination=where;
	}
	public void SetPlayerPassing(float passing)
	{
		playerPassing=(int)passing;
	}
	public void SetPlayerTackling(float tackling)
	{
		playerTackling=(int)tackling;
	}
	public void SetPlayerCrossing(float crossing)
	{
		playerCrossing=(int)crossing;
	}
	public void SetGameSpeed(float speed)
	{
		gameSpeed=(int)speed;
	}
	public void SetPlayerFinishing(float finish)
	{
		playerFinishing=(int)finish;
	}
	public bool HasTheGameStarted()
	{
		return gameStarted;
	}

	int GetEnemyFormationPointsInPosition(Vector2 pos)
	{
		if(pos.x==-1)
			return enemyTeam.attack;
		else if(pos.x==0)
			return enemyTeam.midfield;
		else
			return enemyTeam.defence;
	}

	int GetAllyFormationPointsInPosition(Vector2 pos)
	{
		if(pos.x==-1)
			return playerTeam.defence;
		else if(pos.x==0)
			return playerTeam.midfield;
		else
			return playerTeam.attack;
	}

	public void makeMove(string which)
	{
		if(waitingForPlayerInput)
		{
			Invoke(which, 0f);
		}
		
	}

	public void makeMove()
	{
		if(waitingForPlayerInput)
		{
			Invoke(selectedMove, 0f);
		}

	}
		
	void Pass()
	{
		Vector2[] possibleMoves=GetPassingPositions();
		bool found=false;
		foreach(Vector2 w in possibleMoves)
			if(w.Equals(moveDestination))
				found=true;

		if(!found)
			return;

		int yourPercent=playerPassing*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(moveDestination)*20;
		SetCurrentBallPosition(moveDestination);
		if(yourPercent>=enemyPercent)
		{
			AddText("Pass successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.passesSuccessful++;
		}
		else
		{
			AddText("Pass unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.passesUnsuccessfull++;
			ChangePossession();
		}
		Refresh();
		waitingForPlayerInput=false;
	}

	bool Tackle()
	{
		int yourPercent=playerTackling*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(board.currentPlayerPosition)*20;

		if(yourPercent>=enemyPercent)
		{
			AddText("Tackle successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.tacklesSuccessful++;
			ChangePossession();
			return true;
		}
		else
		{
			AddText("Tackle unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.tacklesUnsuccessfull++;
			return false;
		}
	}

	void Cross()
	{
		bool found=false;
		if(new Vector2(1,0).Equals(moveDestination))
				found=true;

		if(!found)
			return;

		int yourPercent=playerCrossing*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(moveDestination)*20;
		SetCurrentBallPosition(moveDestination);
		if(yourPercent>=enemyPercent)
		{
			AddText("Cross successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.crossesSuccessful++;
		}
		else
		{
			AddText("Cross unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.crossesUnsuccessfull++;
			ChangePossession();
		}
		Refresh();
		waitingForPlayerInput=false;
	}

	void UpdateTime()
	{
		timer.text=board.numberOfTurns+"'";
	}
}
