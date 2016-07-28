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
	public Vector2 preferredPosition;
	public bool movedThisTurn;
	public event Action onActionFail;
	public event Action onActionSuccess;
	public event Action onEnergySet;
	public event Action onEnergyDeplete;
	public Vector2 dribblingTarget;
	public Contusion contusion;

	private float energy;
	private int involveLevel;
	private bool energyDepleted;
	private bool hasYellow;

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
		d.Add("Corners", new Attribute("Corners", 1));
		d.Add("Long Throws", new Attribute("Long Throws", 1));
		d.Add("Heading", new Attribute("Heading", 1));
		d.Add("Free Kick Taking", new Attribute("Free Kick Taking", 1));
		d.Add("Penalty Taking", new Attribute("Penalty Taking", 1));
		playerInfo.SetPlayerAttributes(d);
	}

	void Start()
	{
		GameManager.instance.onTurnStart+=ResetMove;
		GameManager.instance.onMatchStart+=InitPlayer;
		GameManager.instance.onMatchEnd+=SetStartingPosition;

	}

	void InitPlayer()
	{
		movedThisTurn=false;
		maxEnergy=playerInfo.playerAttributes["Stamina"].value*30;
		SetEnergy(maxEnergy);
		energyDepleted=false;
	}

	void SetStartingPosition()
	{
		MoveYourself(Vector2.zero);
	}

	void ResetMove()
	{
		movedThisTurn=false;
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
			actionList.Dribbling.subActions[0].MakeAction();
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			SetDribblingTarget(destination);
			actionList.Dribbling.subActions[1].MakeAction();
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=true;
		GameManager.instance.EndPlayerTurn();
	}

	public bool Tackle()
	{
		
		if(CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Tackling").value,position, position))
		{
			actionList.Tackle.subActions[1].MakeAction();
			if(onActionSuccess!=null)
				onActionSuccess();
			return true;
		}
		else
		{
			actionList.Tackle.subActions[0].MakeAction();
			if(onActionFail!=null)
				onActionFail();
			return false;
		}
	}

	public void FinishHead()
	{
		GameManager.instance.ChangeBallPossession(Side.ENEMY);	
		int percent=playerInfo.GetAttribute("Heading").value*5;
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
		GameManager.instance.EndPlayerRestartMove();
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.playerHasBall=false;
		GameManager.instance.EndPlayerTurn();
	}

	public void FinishShoot()
	{
		GameManager.instance.ChangeBallPossession(Side.ENEMY);	
		int percent=playerInfo.GetAttribute("Finishing").value*5;
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
		GameManager.instance.SetBallPosition(Vector2.right);
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

	public void Corner()
	{
		MoveYourself(GameManager.instance.nextAction.source);
		GameManager.instance.SetBallPosition(Vector2.right);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Corners").value, Vector2.zero, Vector2.right))
		{
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			GameManager.instance.ChangeBallPossession(Side.PLAYER);
			if(onActionSuccess!=null)
				onActionSuccess();
		}
			
		GameManager.instance.EndPlayerRestartMove();
	}

	public void Out()
	{
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Long Throws").value+6, position, position))
		{
			actionList.ThrowIn.subActions[0].MakeAction();
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			actionList.ThrowIn.subActions[1].MakeAction();
			GameManager.instance.ChangeBallPossession(Side.PLAYER);
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.EndPlayerRestartMove();
	}

	public void LongOut(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Long Throws").value, destination, destination))
		{
			actionList.ThrowIn.subActions[0].MakeAction();
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			actionList.ThrowIn.subActions[1].MakeAction();
			GameManager.instance.ChangeBallPossession(Side.PLAYER);
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.EndPlayerRestartMove();
	}

	public void Head(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Heading").value, destination, destination))
		{
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			GameManager.instance.ChangeBallPossession(Side.PLAYER);
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.EndPlayerRestartMove();
	}

	public void FreeKick()
	{
		GameManager.instance.SetBallPosition(Vector2.right);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Free Kick Taking").value, Vector2.right, Vector2.right))
		{
			actionList.FreeKick.subActions[0].MakeAction();
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			actionList.FreeKick.subActions[1].MakeAction();
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.EndPlayerRestartMove();
	}

	public void Penalty()
	{
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetAttribute("Free Kick Taking").value, Vector2.right, Vector2.right))
		{
			actionList.Penalty.subActions[0].MakeAction();
			if(onActionFail!=null)
				onActionFail();
		}
		else
		{
			actionList.Penalty.subActions[1].MakeAction();
			if(onActionSuccess!=null)
				onActionSuccess();
		}

		GameManager.instance.EndPlayerRestartMove();
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

	public void MoveYourselfAction(Vector2 destination)
	{
		movedThisTurn=true;
		MoveYourself(destination);
		GameManager.instance.Unpause();
	}

	public void SetEnergy(float val)
	{
		if(val<=0)
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

	public void SetDribblingTarget(Vector2 dest)
	{
		dribblingTarget=dest;
	}

	public void GetYellowCard()
	{
		if(hasYellow)
			GetRedCard();
		else
			hasYellow=true;
	}

	public void GetRedCard()
	{
		if(onEnergyDeplete!=null)
			onEnergyDeplete();
	}

	public void GetContusion(Contusion cont)
	{
		contusion=cont;
		if(onEnergyDeplete!=null)
			onEnergyDeplete();
	}
}
