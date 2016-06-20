using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PitchManager : MonoBehaviour 
{
	//TODO clean it up and move some methods to GameManager


    public GameObject[] fields;
    public GameObject ball;
    public Text[] logs;
	public GameObject playerSprite;


	void Awake()
	{
		timer=GameObject.Find("Time").GetComponent<Text>();
		buttonManager=GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
	}

	void Start ()
    {
		GameManager.instance.onPlayerMove+=MovePlayerSprite;
		GameManager.instance.onMatchStart+=InitPitch;


		SetPlayerPosition(new Vector2(0,-1));
	}

	
	// Update is called once per frame

		
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
    void InitPitch()
    {
		UnHighlightEverything();
    }
		

	public void HighlightSelected(Vector2[] which)
	{
		Refresh();
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
		


	void HalfTime()
	{
		paused=true;
		buttonManager.SetButtonText("startButton", "2nd half");
		buttonManager.SetInteractable("startButton", true);
		SetCurrentBallPosition(new Vector2(0,0));

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

	void MovePlayerSprite()
	{
		int index=Flatten(Player.GetPlayerPosition());
		playerSprite.transform.position= fields[index].transform.position;
	}

	void UnHighlightEverything()
	{
		foreach(GameObject g in fields)
			g.GetComponent<Field>().UnHighlight();
	}

	public void Refresh()
	{
		UnHighlightEverything();
	}
		

}
