using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CareerManager : MonoBehaviour 
{
	public string[] week=new string[]{"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
	public int currentDay;
	public int currentRound;
	public static GameInformation gameInfo;

	private Text dayDisplay;
	private Text roundDisplay;
	private Button individualTraining;
	private Button clubTraining;
	private Text nextDayButtonText;
	private bool wentToIndividualTraining;
	private bool wentToClubTraining;
	private GameObject attributesParent;
	private Dictionary<string, Text> attributeNames;
	private Dictionary<string, Text> attributeValues;
	private Dictionary<string, Text> attributeExp;

	void Awake()
	{
		if(GameObject.FindGameObjectsWithTag("CareerManager").Length>1)
			Destroy(this.gameObject);
	}

	void Start()
	{
		Debug.Log("Start");
		if(gameInfo==null)
		{
			Debug.Log("Creating new game information");
			gameInfo=new GameInformation();
		}
		InitVariables();
		CheckForDay();
	}

	void InitVariables()
	{
		currentDay=gameInfo.currentWeekDay;
		currentRound=gameInfo.currentRound;
		wentToClubTraining=gameInfo.wentToClubTraining;
		wentToIndividualTraining=gameInfo.wentToIndividualTraining;
		Debug.Log("Loading game info: ");
		Debug.Log(gameInfo.ToString());

		roundDisplay=GameObject.Find("roundDisplay").GetComponent<Text>();
		dayDisplay=GameObject.Find("dayDisplay").GetComponent<Text>();
		individualTraining=GameObject.Find("individualTraining").GetComponent<Button>();
		clubTraining=GameObject.Find("clubTraining").GetComponent<Button>();
		nextDayButtonText=GameObject.Find("nextDayButtonText").GetComponent<Text>();

		attributesParent=GameObject.Find("Attributes");
		attributeNames=new Dictionary<string, Text>();
		attributeValues=new Dictionary<string, Text>();
		attributeExp=new Dictionary<string, Text>();
		foreach(KeyValuePair<string, Attribute> kvp in CareerManager.gameInfo.playerStats.playerAttributes)
		{
			Text name=attributesParent.transform.Find(kvp.Key).gameObject.GetComponent<Text>();
			Text value=name.transform.FindChild("Value").gameObject.GetComponent<Text>();
			Text exp=name.transform.FindChild("Exp").gameObject.GetComponent<Text>();
			name.text=kvp.Key;
			attributeNames.Add(kvp.Key, name);
			attributeValues.Add(kvp.Key, value);
			attributeExp.Add(kvp.Key, exp);
		}
		UpdateAttributeInfo();
	}

	void Update () 
	{
	
	}

	void PlayMatch()
	{
		GameObject possibleStats=GameObject.Find("MatchStats");
		if(possibleStats!=null)
			Destroy(possibleStats);
		SaveGame();
		SceneManager.LoadScene("sampleGame");
	}

	public void BeginNextDay()
	{
		wentToClubTraining=false;
		wentToIndividualTraining=false;
		++currentDay;
		currentDay%=7;
		if(currentDay==0)
		{
			currentRound++;
			PlayMatch();
		}
		CheckForDay();
	}

	void CheckForDay()
	{
		roundDisplay.text="Round " + currentRound;
		dayDisplay.text=week[currentDay];
		if(currentDay==6)
		{
			individualTraining.interactable=false;
			clubTraining.interactable=false;
			nextDayButtonText.text="Play Match";
		}
		else
		{
			nextDayButtonText.text="Next Day";
			if(wentToClubTraining)
				clubTraining.interactable=false;
			else
				clubTraining.interactable=true;
			if(wentToIndividualTraining)
				individualTraining.interactable=false;
			else
				individualTraining.interactable=true;
		}
	}

	public void UpdateAttributeInfo()
	{
		foreach(KeyValuePair<string, Text> k in attributeNames)
		{
			attributeValues[k.Key].text=CareerManager.gameInfo.playerStats.GetAttribute(k.Key).value.ToString();
			attributeExp[k.Key].text=CareerManager.gameInfo.playerStats.GetAttribute(k.Key).currentExp.ToString();
		}
	}

	public void GoToIndividualTraining()
	{
		wentToIndividualTraining=true;
		SaveGame();
		SceneManager.LoadScene("individualTraining");
	}
	public void GoToClubTraining()
	{
		wentToClubTraining=true;
		SaveGame();
		SceneManager.LoadScene("clubTraining");

	}

	void SaveGame()
	{
		gameInfo.wentToClubTraining=wentToClubTraining;
		gameInfo.wentToIndividualTraining=wentToIndividualTraining;
		gameInfo.currentRound=currentRound;
		gameInfo.currentWeekDay=currentDay;
		Debug.Log("Saving game info:");
		Debug.Log(gameInfo.ToString());
	}
}

