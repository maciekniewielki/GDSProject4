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
	public GameObject clubTrainingPopUp;
	public GameObject individualTrainingPopUp;
	public Text clubTrainingAttributeName;
	public Text clubTrainingAttributeExp;
	public Text individualTrainingAttributeNames;
	public GameObject individualTrainingButtonsParent;

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
	private Dictionary<string, string[]> trainingResultsList;

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
			Text exp=name.transform.FindChild("Exp").gameObject.GetComponent<Text>();
			name.text=kvp.Key;
			attributeNames.Add(kvp.Key, name);
			attributeValues.Add(kvp.Key, value);
			attributeExp.Add(kvp.Key, exp);
		}
		UpdateAttributeInfo();
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
			SaveGame();
			PlayMatch();
			return;
		}
		individualTrainingAttributeNames.text="";
		SaveGame();
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
		SetAllIndividualTrainingButtonsInteractable(true);
		individualTrainingPopUp.SetActive(true);
		wentToIndividualTraining=true;
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
		wentToClubTraining=true;
		CheckForDay();
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
}

