using UnityEngine;
using System.Collections;

public class RestartAction 
{
	public RestartActionType type;
	public Side performingSide;
	public bool isPlayerPerforming;
	public Vector2 source;

	public RestartAction(RestartActionType type, Side performingSide, bool isPlayerPerforming, Vector2 source)
	{
		this.type=type;
		this.performingSide=performingSide;
		this.isPlayerPerforming=isPlayerPerforming;
		this.source=source;
	}
};


public enum RestartActionType{CORNER, FREEKICK, PENALTY};