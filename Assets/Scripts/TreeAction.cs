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

	public TreeAction(float probability ,TreeAction[] subActions=null, bool isLast=false, string message="", Action action=null)
	{
		this.probability=probability;
		this.subActions=subActions;
		this.isLast=isLast;
		this.message=message;
		run=action;
	}

	public void MakeAction()
	{
		if(!isLast)
		{
			RunRecursively();
			return;
		}	

		if(run!=null)
			run();

		GameManager.instance.logs.AddText(message);
	}

	void RunRecursively()
	{
		float temp=0;
		float randValue=UnityEngine.Random.value;
		for(int ii=0; ii<subActions.Length;ii++)
		{
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
}
