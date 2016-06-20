using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Player : MonoBehaviour 
{
	//TODO make it work
	//TODO add finishshoot implementation
	public Vector2 position;
	public PlayerInfo playerInfo;

	private PitchManager pitch;

	void Start()
	{
		pitch=GameObject.Find("Pitch").GetComponent<PitchManager>();
	}
		
	public void Pass(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetPlayerAttributes()["Passing"],position, destination))
			GameManager.instance.ChangeBallPossession(Side.ENEMY);
		
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.EndPlayerTurn();
	}

	public bool Tackle()
	{
		if(CalculationsManager.IsMoveSuccessful(playerInfo.GetPlayerAttributes()["Tackling"],position, position))
			return true;
		else
			return false;
	}

	public void Cross(Vector2 destination)
	{
		GameManager.instance.SetBallPosition(destination);
		if(!CalculationsManager.IsMoveSuccessful(playerInfo.GetPlayerAttributes()["Crossing"],position, destination))
			GameManager.instance.ChangeBallPossession(Side.ENEMY);

		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.EndPlayerTurn();
	}

	public static Vector2 GetPlayerPosition()
	{
		return position;
	}
		
	public void MoveYourself(Vector2 destination)
	{
		position=destination;

		if(GameManager.instance.onPlayerMove!=null)
			GameManager.instance.onPlayerMove();
	}
}
