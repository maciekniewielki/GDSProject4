using UnityEngine;
using System.Collections;
using System;

public class TreeAction
{
	
	public TreeAction[] subActions;
	public float probability;
	public bool isLast;
	public string message;
	public event Action run;
	public checkTypes checkType;
	public float probabilityWithCheckedType;

	public TreeAction(float probability ,TreeAction[] subActions=null, bool isLast=false, string message="", Action action=null, checkTypes checkType=checkTypes.NONE, float probabilityWithCheckedType=0f)
	{
		this.probability=probability;
		this.subActions=subActions;
		this.isLast=isLast;
		this.message=message;
		run=action;
		this.checkType=checkType;
		this.probabilityWithCheckedType=probabilityWithCheckedType;
	}

	public void MakeAction()
	{
		if(!isLast)
		{
			RunRecursively();
			return;
		}	

		if(message!=null&&!message.Equals(""))
		{
			Debug.Log(message);
			GameManager.instance.logs.AddText(message);
		}

		if(run!=null)
			run();
	}

	void RunRecursively()
	{
		float temp=0;
		float randValue=UnityEngine.Random.value;
		for(int ii=0; ii<subActions.Length;ii++)
		{
			if(CheckTheType(checkType))
				temp+=subActions[ii].probabilityWithCheckedType;
			else
				temp+=subActions[ii].probability;
			if(randValue<=temp||ii==subActions.Length-1)
			{
				subActions[ii].MakeAction();
				return;
			}
			else
				continue;
		}
	}

	bool CheckTheType(checkTypes type)
	{
		if(type==checkTypes.BALL_ON_SIDES)
			return CalculationsManager.IsPositionOnTheEdge(GameManager.instance.ballPosition);
		else if(type==checkTypes.PLAYER_ON_PENALTY)
			return CalculationsManager.IsPlayerOnPenaltyArea();
		else if(type==checkTypes.PLAYER_SAME_SECTOR_AS_BALL)
			return CalculationsManager.IsPlayerStandingOnBall();
		else
			return false;
	}
};

public enum checkTypes{PLAYER_ON_PENALTY, BALL_ON_SIDES, PLAYER_SAME_SECTOR_AS_BALL, NONE}
