using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public Text goalsTextDisplay;
	public Text timerDisplay;
	public Slider energySlider;


	private Dictionary<string, Text> attributes;
	private Dictionary<string, Text> attributeValues;
	private GameObject attributesParent;


	void Start () 
	{
		InitConnections();
		UpdateAttributes();
		GameManager.instance.onGoal+=UpdateUI;
		GameManager.instance.onTurnStart+=UpdateUI;
		GameManager.instance.onTurnEnd+=UpdateUI;
		GameManager.instance.onPlayerTeamGoal+=UpdateUI;
		GameManager.instance.onEnemyTeamGoal+=UpdateUI;
		GameManager.instance.player.onEnergySet+=UpdateEnergyBar;
		GameManager.instance.onMatchStart+=SetStartingEnergy;
	}

	void InitConnections()
	{
		attributesParent=GameObject.Find("Attributes");
		attributes=new Dictionary<string, Text>();
		attributeValues=new Dictionary<string, Text>();
		foreach(KeyValuePair<string, Attribute> k in GameManager.instance.player.playerInfo.playerAttributes)
		{
			Text found=attributesParent.transform.Find(k.Key).gameObject.GetComponent<Text>();
			found.text=k.Key;
			attributes.Add(k.Key, found);
			attributeValues.Add(k.Key, found.transform.FindChild("Value").gameObject.GetComponent<Text>());
		}
	}

	public void UpdateAttributes()
	{
		foreach(KeyValuePair<string, Text> k in attributes)
		{
			attributeValues[k.Key].text=GameManager.instance.player.playerInfo.GetAttribute(k.Key).value.ToString();
		}
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
