using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Profile
{
	private string name;
	private GameInformation sessionInfo;
	private DateTime lastSaveTime;
	private bool isEmpty;

	public Profile()
	{
		isEmpty = true;
	}

	public Profile(GameInformation sessionInfo)
	{
		this.sessionInfo = sessionInfo;
		lastSaveTime = DateTime.Now;
		isEmpty = false;
	}

	public void UpdateProfile(GameInformation sessionInfo)
	{
		this.sessionInfo = sessionInfo;
		lastSaveTime = DateTime.Now;
		isEmpty = false;
	}

	public GameInformation GetSession()
	{
		return sessionInfo;
	}

	public bool IsEmpty()
	{
		return isEmpty;
	}

	public string GetName()
	{
		return name;
	}

	public DateTime GetLastSaveTime()
	{
		return lastSaveTime;
	}

	public void SetName(string name)
	{
		this.name = name;
	}
}

