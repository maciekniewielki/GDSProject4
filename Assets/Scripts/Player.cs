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
	public int maxEnergy;
	public event Action onActionFail;
	public event Action onActionSuccess;
	public event Action onEnergySet;

	private int energy;

	void Awake()
	{
		playerInfo=new PlayerInfo();
		Dictionary<string, Attribute> d=new Dictionary<string, Attribute>();
		d.Add("Passing", new Attribute("Passing", 1));
		d.Add("Crossing", new Attribute("Crossing", 1));
		d.Add("Finishing", new Attribute("Finishing",1));
		d.Add("Tackling", new Attribute("Tackling", 1));
		d.Add("Dribbling", new Attribute("Dribbling", 1));
		d.Add("Long Shots", new Attribute("Long Shots", 1));
		d.Add("Stamina", new Attribute("Stamina", 1));
		maxEnergy=d["Stamina"].value*5;
		playerInfo.SetPlayerAttributes(d);
		
	}

	void Start()
	{
		
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

	public void Dribble(Vector2 destination)
	{
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Dribbling").value, position, position))
		{
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			if(onActionSuccess!=null)
				onActionSuccess();
			MoveYourself(destination);
			GameManager.instance.SetBallPosition(destination);
		}

		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=true;
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

	public void LongShot()
	{
		GameManager.instance.ChangeBallPossession(Side.ENEMY);	
		int percent=playerInfo.GetAttribute("Long Shots").value*5;
		if(Vector2.Distance(position, Vector2.right)>1)
			percent-=25;
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

	public void SetEnergy(int val)
	{
		energy=val;
		if(onEnergySet!=null)
			onEnergySet();
	}

	public void ReduceEnergyBy(int val)
	{
		SetEnergy(energy-val);
	}

	public int GetEnergy()
	{
		return energy;
	}
}
