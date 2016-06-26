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
	public event Action onActionFail;
	public event Action onActionSuccess;

	private PitchManager pitch;

	void Awake()
	{
		playerInfo=new PlayerInfo();
		Dictionary<string, Attribute> d=new Dictionary<string, Attribute>();
		d.Add("Passing", new Attribute("Passing", 1));
		d.Add("Crossing", new Attribute("Crossing", 1));
		d.Add("Finishing", new Attribute("Finishing",1));
		d.Add("Tackling", new Attribute("Tackling", 1));
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
		{
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			if(onActionSuccess!=null)
				onActionSuccess();
		}
		
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public bool Tackle()
	{
		
		if(CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Tackling").value,position, position))
		{
			if(onActionSuccess!=null)
				onActionSuccess();
			return true;
		}
		else
		{
			if(onActionFail!=null)
				onActionFail();
			return false;
		}
	}

	public void FinishShoot()
	{
		GameManager.instance.ChangeBallPossession(Side.ENEMY);	
		int percent=playerInfo.GetAttribute("Finishing").value*5;
		if(UnityEngine.Random.Range(1,101)<=percent)
		{
			if(onActionSuccess!=null)
				onActionSuccess();
			GameManager.instance.Goal(true, Side.PLAYER);
		}
		else
		{
			if(onActionFail!=null)
				onActionFail();
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
		{
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			if(onActionSuccess!=null)
				onActionSuccess();
		}

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
