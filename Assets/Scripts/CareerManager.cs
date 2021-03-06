﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CareerManager : MonoBehaviour 
{
    public string[] week;
	public static GameInformation gameInfo;
	public GameObject clubTrainingPopUp;
	public GameObject individualTrainingPopUp;
	public Text clubTrainingAttributeName;
	public Text clubTrainingAttributeExp;
	public Text individualTrainingAttributeNames;
	public GameObject individualTrainingButtonsParent;
	public Text tableOfResults;
	public Text leagueTableDisplay;
	public Text playerName;
	public Image playerNameBackground;
    public Text ageDisplayNumber;

	private Text dayDisplay;
	private Text roundDisplay;
	private Text SeasonDisplay;
	private Button individualTraining;
	private Button clubTraining;
	private Text nextDayButtonText;
	private GameObject attributesParent;
	private Dictionary<string, Text> attributeNames;
	private Dictionary<string, Text> attributeValues;
	private Dictionary<string, Slider> attributeExp;
	private Dictionary<string, string[]> trainingResultsList;

	void Awake()
	{
        week = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        if (GameObject.FindGameObjectsWithTag("CareerManager").Length>1)
			Destroy(this.gameObject);
	}

	void Start()
	{
        SaveGame();
		Debug.Log("Start");
		if(gameInfo==null)
		{
			Debug.Log("Creating new game information");
			gameInfo=new GameInformation();
		}
		InitVariables();
		Debug.Log(gameInfo.calendar);
		playerName.text=CareerManager.gameInfo.playerStats.playerName+" "+CareerManager.gameInfo.playerStats.playerSurname;
	}

	void InitVariables()
    {
		Debug.Log("Loading game info: ");
		Debug.Log(gameInfo.ToString());

		roundDisplay=GameObject.Find("roundDisplay").GetComponent<Text>();
		dayDisplay=GameObject.Find("dayDisplay").GetComponent<Text>();
		SeasonDisplay = GameObject.Find("seasonDisplay").GetComponent<Text>();
		individualTraining=GameObject.Find("individualTraining").GetComponent<Button>();
		clubTraining=GameObject.Find("clubTraining").GetComponent<Button>();
		nextDayButtonText=GameObject.Find("nextDayButtonText").GetComponent<Text>();

		attributesParent=GameObject.Find("Attributes");
		attributeNames=new Dictionary<string, Text>();
		attributeValues=new Dictionary<string, Text>();
		attributeExp=new Dictionary<string, Slider>();
		trainingResultsList=new Dictionary<string, string[]>();
		trainingResultsList.Add("Dribble Through Cones", new string[]{"Dribbling", "Passing"});
        trainingResultsList.Add("Individual Football Tennis", new string[] { "Stamina", "Passing" });
        trainingResultsList.Add("One Leg Jumps", new string[] { "Stamina", "Dribbling" });
        trainingResultsList.Add("Ladders Work", new string[] { "Dribbling", "Stamina" });
        trainingResultsList.Add("First Touch", new string[] { "Passing", "Finishing", "Dribbling" });
        trainingResultsList.Add("Pass & Recieve", new string[] { "Passing" });
        trainingResultsList.Add("Heading Skills", new string[] { "Heading", "Passing" });
        trainingResultsList.Add("Rondo", new string[] { "Tackling", "Passing" });
        trainingResultsList.Add("Shooting", new string[] { "Finishing", "Long Shots", "Penalties" });
        trainingResultsList.Add("Jogging", new string[] { "Stamina" });
        trainingResultsList.Add("Corner Taking", new string[] { "Corners", "Crossing", "Free Kicks" });
        trainingResultsList.Add("Free Kick Taking", new string[] { "Free Kicks", "Long Shots" });
        trainingResultsList.Add("Gym", new string[] { "Long Throws", "Stamina" });
		foreach(KeyValuePair<string, Attribute> kvp in CareerManager.gameInfo.playerStats.playerAttributes)
		{
			Text name=attributesParent.transform.Find(kvp.Key).gameObject.GetComponent<Text>();
			Text value=name.transform.FindChild("Value").gameObject.GetComponent<Text>();
			Slider exp=name.transform.FindChild("Exp").gameObject.GetComponent<Slider>();
			name.text=kvp.Key;
			attributeNames.Add(kvp.Key, name);
			attributeValues.Add(kvp.Key, value);
			attributeExp.Add(kvp.Key, exp);
		}
		if(gameInfo.currentRound>1)
		{
			gameInfo.calendar.AddPointsForWeek(gameInfo.currentRound -1);
			tableOfResults.text=gameInfo.calendar.GetWeekByNumber(gameInfo.currentRound -1).ToString();
			leagueTableDisplay.text=gameInfo.calendar.ConvertToLeagueTableString();
		}
		UpdateAttributeInfo();
		playerName.color=CareerManager.gameInfo.playerStats.currentTeam.textColor;
		playerNameBackground.color=CareerManager.gameInfo.playerStats.currentTeam.bgColor;
        ageDisplayNumber.text = gameInfo.playerStats.playerAge.ToString();
		CheckForDay();
	}


	void PlayMatch()
	{
		GameObject possibleStats=GameObject.Find("MatchStats");
		if(possibleStats!=null)
			Destroy(possibleStats);
		gameInfo.calendar.CalculateScoreForWeek(gameInfo.currentRound -1, gameInfo.playerStats.currentTeam.name);
		gameInfo.nextMatch=gameInfo.calendar.GetWeekByNumber(gameInfo.currentRound -1).GetPlayerMatch(gameInfo.playerStats.currentTeam.name);
		SceneManager.LoadScene("sampleGame");
	}

	public void BeginNextDay()
	{
		gameInfo.wentToClubTraining=false;
		gameInfo.wentToIndividualTraining=false;
		++gameInfo.currentWeekDay;
		gameInfo.currentWeekDay%=7;

		if(gameInfo.currentWeekDay==0)
		{
            gameInfo.currentRound++;
			PlayMatch();
			return;
		}
		individualTrainingAttributeNames.text="";
		SaveGame();
		CheckForDay();
	}

	void CheckForDay()
	{
		SeasonDisplay.text = "Season " + gameInfo.currentSeason;
		roundDisplay.text="Round " + gameInfo.currentRound;
		dayDisplay.text=week[gameInfo.currentWeekDay];
        Debug.Log(week[gameInfo.currentWeekDay]);
		if(gameInfo.currentWeekDay==6)
		{
			individualTraining.interactable=false;
			clubTraining.interactable=false;
			nextDayButtonText.text="Play Match";
		}
		else
		{
			nextDayButtonText.text="Next Day";
			if(gameInfo.wentToClubTraining)
				clubTraining.interactable=false;
			else
				clubTraining.interactable=true;
			if(gameInfo.wentToIndividualTraining)
				individualTraining.interactable=false;
			else
				individualTraining.interactable=true;
		}
	}

	public void UpdateAttributeInfo()
	{
		foreach(KeyValuePair<string, Text> k in attributeNames)
		{
            Attribute currentAttribute = CareerManager.gameInfo.playerStats.GetAttribute(k.Key);
            attributeValues[k.Key].text=currentAttribute.value.ToString();

            if (currentAttribute.IsLastLevelReached())
            {
                attributeExp[k.Key].value = 0f;
                attributeExp[k.Key].transform.Find("Background").GetComponent<Image>().color = new Color(0f, 0f, 1f);
            }
            else
                attributeExp[k.Key].value = currentAttribute.GetCurrentExpPercent();
		}
	}

	public void GoToIndividualTraining()
	{
		SetAllIndividualTrainingButtonsInteractable(true);
		individualTrainingPopUp.SetActive(true);
		gameInfo.wentToIndividualTraining=true;
		CheckForDay();
	}
	public void GoToClubTraining()
	{
		string randomAttributeName=CalculationsManager.GetRandomAttributeName(gameInfo.playerStats);
		int randomExp=Random.Range(150,201);
		clubTrainingAttributeName.text="Training topic: "+randomAttributeName;
		clubTrainingAttributeExp.text=""+randomExp;
		gameInfo.playerStats.playerAttributes[randomAttributeName].AddExp(randomExp);
		UpdateAttributeInfo();
		clubTrainingPopUp.SetActive(true);
		gameInfo.wentToClubTraining=true;
		CheckForDay();
	}

	void SaveGame()
	{
		SaveLoad.SaveGame();
		Debug.Log("Saving game info:");
		Debug.Log(gameInfo.ToString());
	}

	public void Click(string clickedTraining)
	{
		string []attributesToImprove=trainingResultsList[clickedTraining];
		if(attributesToImprove.Length==1)
		{
			int randomExp=Random.Range(85,151);
			individualTrainingAttributeNames.text=attributesToImprove[0]+": "+randomExp;
			gameInfo.playerStats.playerAttributes[attributesToImprove[0]].AddExp(randomExp);
		}
		else if(attributesToImprove.Length==2)
		{
			int randomExpPrimary=Random.Range(65,106);
			int randomExpSecondary=Random.Range(25,46);
			individualTrainingAttributeNames.text=attributesToImprove[0]+": "+randomExpPrimary+"\n"+attributesToImprove[1]+": "+randomExpSecondary;
			gameInfo.playerStats.playerAttributes[attributesToImprove[0]].AddExp(randomExpPrimary);
			gameInfo.playerStats.playerAttributes[attributesToImprove[1]].AddExp(randomExpSecondary);
		}
		else
		{
			int randomExpPrimary=Random.Range(60,96);
			int randomExpSecondary=Random.Range(25,41);
			int randomExpTertiary=Random.Range(1,16);
			individualTrainingAttributeNames.text=attributesToImprove[0]+": "+randomExpPrimary+"\n"
												 +attributesToImprove[1]+": "+randomExpSecondary+"\n"
												 +attributesToImprove[2]+": "+randomExpTertiary;
			
			gameInfo.playerStats.playerAttributes[attributesToImprove[0]].AddExp(randomExpPrimary);
			gameInfo.playerStats.playerAttributes[attributesToImprove[1]].AddExp(randomExpSecondary);
			gameInfo.playerStats.playerAttributes[attributesToImprove[2]].AddExp(randomExpTertiary);
		}
		SetAllIndividualTrainingButtonsInteractable(false);

		UpdateAttributeInfo();
	}

	void SetAllIndividualTrainingButtonsInteractable(bool interactable)
	{
		foreach(Button b in individualTrainingButtonsParent.GetComponentsInChildren<Button>())
			b.interactable=interactable;
	}

	public static bool CheckForLeagueEnd()
	{
		return gameInfo.currentRound >= gameInfo.calendar.weeks.Length+1;
	}
}

