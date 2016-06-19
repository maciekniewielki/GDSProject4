using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour 
{
	//TODO build the game from scratch

	public static GameManager instance;
	public static MatchStatistics stats;
	public event Action onGoal;
	public event Action onPlayerMove;
	public event Action onTurnStart;


	void Start () 
	{
		if(instance==null)
			instance=this;
		else if(instance!=this)
			Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
