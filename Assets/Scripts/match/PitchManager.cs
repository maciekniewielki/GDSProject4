﻿using UnityEngine;
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

	void Awake()
	{
		
	}

	void Start ()
    {
		GameManager.instance.onCornerBegin+=PrepareForRestartMove;
		GameManager.instance.onCornerEnd+=EndRestartMove;
		GameManager.instance.onPlayerMove+=MovePlayerSprite;
		GameManager.instance.onMatchStart+=InitPitch;
		GameManager.instance.onMatchEnd+=SetPlayerVisibility;
		GameManager.instance.onBallMove+=SetBallGraphicalPosition;
		GameManager.instance.onPlayerTurnEnd+=UnHighlightEverything;
		GameManager.instance.player.onEnergyDeplete+=RemovePlayerSprite;
	}

	
	// Update is called once per frame

		
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
		playerSprite.GetComponent<SpriteRenderer>().enabled=true;
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

	void UnHighlightEverything()
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
		playerSprite.GetComponent<SpriteRenderer>().enabled=false;
	}

	void RemoveBallSprite()
	{
		ball.GetComponent<SpriteRenderer>().enabled=false;
	}

	public void PrepareForRestartMove()
	{
		if(GameManager.instance.nextAction.source==new Vector2(1,1))
			leftEnemyCorner.SetActive(true);
		else
			rightEnemyCorner.SetActive(true);
		RemoveBallSprite();
		RemovePlayerSprite();
	}

	public void EndRestartMove()
	{
		playerSprite.GetComponent<SpriteRenderer>().enabled=true;
		ball.GetComponent<SpriteRenderer>().enabled=true;

		leftEnemyCorner.SetActive(false);
		rightEnemyCorner.SetActive(false);
		leftPlayerCorner.SetActive(false);
		rightPlayerCorner.SetActive(false);
	}
}