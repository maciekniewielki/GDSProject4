using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text goalsTextDisplay;
	public Text timerDisplay;


	void Start () 
	{
		GameManager.instance.onGoal+=UpdateUI;
		GameManager.instance.onTurnStart+=UpdateUI;
		GameManager.instance.onPlayerTeamGoal+=Update;
		GameManager.instance.onEnemyTeamGoal+=UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void UpdateUI()
	{
		goalsTextDisplay.text=GameManager.instance.stats.playerTeam.name+" "+GameManager.instance.stats.playerTeamGoals+":"+GameManager.instance.stats.enemyTeamGoals+" "+GameManager.instance.stats.enemyTeam.name;
		timerDisplay.text=GameManager.instance.currentMinute+"'";
	}
		
}
