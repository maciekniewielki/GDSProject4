using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text goalsTextDisplay;
	public Text timerDisplay;
	public MatchStatistics stats;


	void Start () 
	{
		GameManager.instance.onGoal+=UpdateUI;
		GameManager.instance.onTurnStart+=UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateUI()
	{
		goalsTextDisplay.text=stats.playerTeam.name+" "+stats.playerTeamGoals+":"+stats.enemyTeamGoals+" "+stats.enemyTeam.name;
		timerDisplay.text=stats.currentMinute+"'";
	}
		
}
