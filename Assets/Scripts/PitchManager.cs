using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PitchManager : MonoBehaviour 
{
	//TODO clean it up and remove some methods
	/*
    // Use this for initialization

   
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

    


	void IncrementTurnCounter()
	{
		board.numberOfTurns++;
		if(board.numberOfTurns==46&&!secondHalfStarted)
		{
			HalfTime();
			secondHalfStarted=true;
		}
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
		

	void UpdateTime()
	{
		timer.text=board.numberOfTurns+"'";
	}*/
}
