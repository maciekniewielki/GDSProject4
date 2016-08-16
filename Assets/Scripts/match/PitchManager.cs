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
	public GameObject rightEnemyCorner;
	public GameObject leftEnemyCorner;
	public GameObject rightPlayerCorner;
	public GameObject leftPlayerCorner;
	public GameObject freeKickIcon;

	public GameObject leftDefenceOut;
	public GameObject leftMiddleOut;
	public GameObject leftAttackOut;
	public GameObject rightDefenceOut;
	public GameObject rightMiddleOut;
	public GameObject rightAttackOut;

	void Awake()
	{
		
	}

	void Start ()
    {
		GameManager.instance.onMakeMove+=UnHighlightEverything;
		GameManager.instance.onPenaltyBegin+=PrepareForRestartMove;
		GameManager.instance.onFreeKickBegin+=PrepareForRestartMove;
		GameManager.instance.onPlayerMove+=UnHighlightEverything;
		GameManager.instance.onTurnStart+=UnHighlightEverything;
		GameManager.instance.onCornerBegin+=PrepareForRestartMove;
		GameManager.instance.onOutBegin+=PrepareForRestartMove;
		GameManager.instance.onCornerEnd+=EndRestartMove;
		GameManager.instance.onPlayerMove+=MovePlayerSprite;
		GameManager.instance.onMatchStart+=InitPitch;
		GameManager.instance.onMatchEnd+=SetPlayerVisibility;
		GameManager.instance.onBallMove+=SetBallGraphicalPosition;
		GameManager.instance.onPlayerTurnEnd+=UnHighlightEverything;
		GameManager.instance.player.onEnergyDeplete+=RemovePlayerSprite;
		GameManager.instance.player.onActionFail+=UnHighlightEverything;
		GameManager.instance.player.onActionSuccess+=UnHighlightEverything;
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
		

	void SetPlayerVisibility()
	{
		Debug.Log("Player is visible");
		playerSprite.GetComponent<Image>().enabled=true;
	}

    int GetRandPos()
    {
        return Random.Range(-1, 2);
    }

    int Flatten(Vector2 w)
    {
        return (int)((-w.y+1)*3+w.x+1);
    }

    void SetBallGraphicalPosition()
    {
		int index = Flatten(GameManager.instance.ballPosition);
		Debug.Log("Set graphical to: "+index);
        ball.transform.position = fields[index].transform.position;
    }

	void HighlightField(Vector2 which)
	{
		Debug.Log("Highlighting " + which);
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
		int index=Flatten(GameManager.instance.player.GetPlayerPosition());
		playerSprite.transform.position= fields[index].transform.position;
	}

	public void UnHighlightEverything()
	{
		if(!GameManager.instance.IsPlayerWaitingForRestart())
			foreach(GameObject g in fields)
				g.GetComponent<Field>().UnHighlight();
	}

	public void HardUnHighlightEverything()
	{
		foreach(GameObject g in fields)
			g.GetComponent<Field>().UnHighlight();
	}

	public void Refresh()
	{
		UnHighlightEverything();
	}

	void RemovePlayerSprite()
	{
		Debug.Log("Removed Player Sprite");
		playerSprite.GetComponent<Image>().enabled=false;
	}

	void RemoveBallSprite()
	{
		ball.GetComponent<Image>().enabled=false;
	}

	public void PrepareForRestartMove()
	{
		if(GameManager.instance.nextAction.isPlayerPerforming)
		{
			if(GameManager.instance.nextAction.type==RestartActionType.CORNER)
			{
				HighlightField(Vector2.right);
				if(GameManager.instance.nextAction.source==new Vector2(1,1))
					leftEnemyCorner.SetActive(true);
				else
					rightEnemyCorner.SetActive(true);
			}
			else if(GameManager.instance.nextAction.type==RestartActionType.OUT)
			{
				SetOutIconActive(GameManager.instance.nextAction.source);
			}
			else if(GameManager.instance.nextAction.type==RestartActionType.FREEKICK||GameManager.instance.nextAction.type==RestartActionType.PENALTY)
			{
				HighlightField(Vector2.right);
				SetFreeKickIconActive();
			}
			RemoveBallSprite();
			RemovePlayerSprite();
		}
	}

	void SetOutIconActive(Vector2 field)
	{
		if(field.y==1)
		{
			if(field.x==-1)
				leftDefenceOut.SetActive(true);
			else if(field.x==0)
				leftMiddleOut.SetActive(true);
			else if(field.x==1)
					leftAttackOut.SetActive(true);
		}
		else if(field.y==-1)
		{
			if(field.x==-1)
				rightDefenceOut.SetActive(true);
			else if(field.x==0)
				rightMiddleOut.SetActive(true);
			else if(field.x==1)
				rightAttackOut.SetActive(true);
		}
	}

	void SetFreeKickIconActive()
	{
		int index=Flatten(GameManager.instance.ballPosition);
		freeKickIcon.transform.position= fields[index].transform.position;
		freeKickIcon.SetActive(true);
	}

	public void EndRestartMove()
	{
		if(!GameManager.instance.player.IsEnergyDepleted()&&GameManager.instance.player.contusion==null)
			SetPlayerVisibility();
		ball.GetComponent<Image>().enabled=true;

		leftEnemyCorner.SetActive(false);
		rightEnemyCorner.SetActive(false);
		leftPlayerCorner.SetActive(false);
		rightPlayerCorner.SetActive(false);

		leftAttackOut.SetActive(false);
		leftMiddleOut.SetActive(false);
		leftDefenceOut.SetActive(false);
		rightAttackOut.SetActive(false);
		rightMiddleOut.SetActive(false);
		rightDefenceOut.SetActive(false);

		freeKickIcon.SetActive(false);
	}
}
