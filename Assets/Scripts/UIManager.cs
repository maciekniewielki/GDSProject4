using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text goalsTextDisplay;
	public Text timerDisplay;
	public Slider energySlider;


	void Start () 
	{
		GameManager.instance.onGoal+=UpdateUI;
		GameManager.instance.onTurnStart+=UpdateUI;
		GameManager.instance.onTurnEnd+=UpdateUI;
		GameManager.instance.onPlayerTeamGoal+=UpdateUI;
		GameManager.instance.onEnemyTeamGoal+=UpdateUI;
		GameManager.instance.player.onEnergySet+=UpdateEnergyBar;
		GameManager.instance.onMatchStart+=SetStartingEnergy;
	}

	void UpdateUI()
	{
		goalsTextDisplay.text=GameManager.instance.stats.playerTeam.name+" "+GameManager.instance.stats.playerTeamGoals+":"+GameManager.instance.stats.enemyTeamGoals+" "+GameManager.instance.stats.enemyTeam.name;
		timerDisplay.text=GameManager.instance.currentMinute+"'";
	}

	void SetStartingEnergy()
	{
		energySlider.maxValue=GameManager.instance.player.maxEnergy;
		UpdateEnergyBar();
	}

	void UpdateEnergyBar()
	{
		energySlider.value=GameManager.instance.player.GetEnergy();
	}
		
}
