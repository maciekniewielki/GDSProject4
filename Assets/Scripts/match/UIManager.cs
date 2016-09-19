using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{

	public Text goalsTextDisplay;
	public Text homeTeamTextDisplay;
	public Text awayTeamTextDisplay;
	public Image homeTeamDisplay;
	public Image awayTeamDisplay;
	public Text timerDisplay;
	public Slider energySlider;
    public Text tempoButton;


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

		homeTeamDisplay.color=CareerManager.gameInfo.nextMatch.leftTeam.bgColor;
		awayTeamDisplay.color=CareerManager.gameInfo.nextMatch.rightTeam.bgColor;
		homeTeamTextDisplay.color=CareerManager.gameInfo.nextMatch.leftTeam.textColor;
		awayTeamTextDisplay.color=CareerManager.gameInfo.nextMatch.rightTeam.textColor;
		UpdateUI();
        UpdateTempoIcon();
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
		homeTeamTextDisplay.text=CareerManager.gameInfo.nextMatch.leftTeam.name;
		awayTeamTextDisplay.text=CareerManager.gameInfo.nextMatch.rightTeam.name;
		string goalsDisplay="";
		if(CareerManager.gameInfo.playerStats.currentTeam.name.Equals(CareerManager.gameInfo.nextMatch.leftTeam.name))
			goalsDisplay=GameManager.instance.stats.playerTeamGoals+":"+GameManager.instance.stats.enemyTeamGoals;
		else
			goalsDisplay=GameManager.instance.stats.enemyTeamGoals+":"+GameManager.instance.stats.playerTeamGoals;
		goalsTextDisplay.text=goalsDisplay;
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

    public void SwitchGameSpeed()
    {
        GameManager.instance.SetCurrentGameSpeed((GameManager.instance.GetCurrentGameSpeedLevel() + 1) % GameManager.instance.levelsOfGameSpeed.Length);
        UpdateTempoIcon();
    }

    void UpdateTempoIcon()
    {
        string s = "";
        for (int ii = 0; ii < GameManager.instance.GetCurrentGameSpeedLevel()+1; ii++)
            s += ">";
        tempoButton.text = s;
    }
}
