using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Player : MonoBehaviour 
{
	//TODO make it work
	public Vector2 position;
	public PlayerInfo playerInfo;

	private PitchManager pitch;

	void Awake()
	{
		playerInfo=new PlayerInfo();
		Dictionary<string, Attribute> d=new Dictionary<string, Attribute>();
		d.Add("Passing", new Attribute("Passing", 0));
		d.Add("Crossing", new Attribute("Crossing", 0));
		d.Add("Finishing", new Attribute("Finishing",9));
		d.Add("Tackling", new Attribute("Tackling", 0));
		playerInfo.SetPlayerAttributes(d);
	}

	void Start()
	{
		pitch=GameObject.Find("Pitch").GetComponent<PitchManager>();
	}
		
	public void Pass(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Passing").value, position, destination))
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
		
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public bool Tackle()
	{
		
		if(CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Tackling").value,position, position))
		{
			Debug.Log("Tackling... successful " + playerInfo.GetAttribute("Tackling"));
			return true;
		}
		else
		{
			Debug.Log("Tackling... unsuccessful");
			return false;
		}
	}

	public void FinishShoot()
	{
		GameManager.instance.ChangeBallPossession(Side.ENEMY);	
		int percent=playerInfo.GetAttribute("Finishing").value*5;
		if(UnityEngine.Random.Range(1,101)<=percent)
		{
			Debug.Log("Goal with "+ percent +" percent chance");
			GameManager.instance.Goal(true, Side.PLAYER);
		}
		else
		{
			Debug.Log("Miss with "+ percent +" percent chance");
			GameManager.instance.Miss(true, Side.ENEMY);	
		}
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public void Cross(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Crossing").value ,position, destination))
			GameManager.instance.ChangeBallPossession(Side.ENEMY);

		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public Vector2 GetPlayerPosition()
	{
		return position;
	}
		
	public void MoveYourself(Vector2 destination)
	{
		position=destination;
		GameManager.instance.PlayerMoved();
	}
}
