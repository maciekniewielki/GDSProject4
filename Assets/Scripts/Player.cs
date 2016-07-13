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
	public ActionsList actionList;
	public event Action onActionFail;
	public event Action onActionSuccess;
	public event Action onEnergySet;
	public event Action onEnergyDeplete;

	private float energy;
	private int involveLevel;
	private bool energyDepleted;

	void Awake()
	{
		involveLevel=1;
		playerInfo=new PlayerInfo();
		Dictionary<string, Attribute> d=new Dictionary<string, Attribute>();
		d.Add("Passing", new Attribute("Passing", 1));
		d.Add("Crossing", new Attribute("Crossing", 1));
		d.Add("Finishing", new Attribute("Finishing",1));
		d.Add("Tackling", new Attribute("Tackling", 1));
		d.Add("Dribbling", new Attribute("Dribbling", 1));
		d.Add("Long Shots", new Attribute("Long Shots", 1));
		d.Add("Stamina", new Attribute("Stamina", 1));
		playerInfo.SetPlayerAttributes(d);
	}

	void Start()
	{
		GameManager.instance.onMatchStart+=InitPlayer;
		GameManager.instance.onMatchEnd+=SetStartingPosition;

	}

	void InitPlayer()
	{
		maxEnergy=playerInfo.playerAttributes["Stamina"].value*30;
		SetEnergy(maxEnergy);
		energyDepleted=false;
	}

	void SetStartingPosition()
	{
		MoveYourself(Vector2.zero);
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
		float percent=playerInfo.GetAttribute("Finishing").value*5/100;
		if(UnityEngine.Random.Range(1,101)<=percent)
		{
			if(onActionSuccess!=null)
				onActionSuccess();
			//GameManager.instance.Goal(true, Side.PLAYER);
			actionList.shoot.subActions[1].MakeAction();
		}
		else
		{
			if(onActionFail!=null)
				onActionFail();
			//GameManager.instance.Miss(true, Side.ENEMY);	
			actionList.shoot.subActions[0].MakeAction();
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
			//GameManager.instance.Goal(true, Side.PLAYER);
			actionList.shoot.subActions[1].MakeAction();
		}
		else
		{
			if(onActionFail!=null)
				onActionFail();
			//GameManager.instance.Miss(true, Side.ENEMY);	
			actionList.shoot.subActions[0].MakeAction();
		}
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public void Cross(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		int value=playerInfo.GetAttribute("Crossing").value;

		if(Vector2.Distance(position, Vector2.right)>1)
			value-=5;

		if(!CalculationsManager.IsMoveSuccessful(value ,position, destination))
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

	public void SetEnergy(float val)
	{
		if(val<=0&&!energyDepleted)
		{
			energy=0;
			energyDepleted=true;
			SetInvolvement(1);
			if(onEnergyDeplete!=null)
				onEnergyDeplete();
		}
		else
			energy=val;
		if(onEnergySet!=null)
			onEnergySet();
	}
		
	public void ReduceEnergy()
	{
		if(involveLevel==1)
			SetEnergy(energy-3);
		else if(involveLevel==2)
			SetEnergy(energy-4);
		else
			SetEnergy(energy-6);
	}

	public float GetEnergy()
	{
		return energy;
	}

	public void SetInvolvement(int involvement)
	{
		involveLevel=involvement;
		Debug.Log("Involvement:: "+involveLevel);
	}

	public int GetInvolvement()
	{
		return this.involveLevel;
	}

	public bool IsEnergyDepleted()
	{
		return energyDepleted;
	}

}
