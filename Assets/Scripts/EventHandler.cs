using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class EventHandler : MonoBehaviour {

	public GameObject[] fields;
	public string[] positionNames=new string[]{"LB", "LM", "LF", "CB", "CM", "CF", "RB", "RM", "RF"};
	public Text selectedPositionText;
    public Text remainingPointsText;
    public GameObject attributesParent;
	public Text teamsTextDropdown;
	public Dropdown teamsDropdown;
	private Dictionary<string, Attribute> playerAttributes;
    private int remainingPoints = 20;
	private PlayerInfo playerInfo;
	private Team[] teams;
    public AudioSource kick;

    void Awake()
	{
		playerInfo=new PlayerInfo();
		CareerManager.gameInfo=new GameInformation();
	}

	void Start ()
    {
		teams=GenerateTeamLevels();
		teamsDropdown.ClearOptions();
		teamsDropdown.AddOptions(teams.Select(t => t.name).ToList<string>());
		playerAttributes=new Dictionary<string, Attribute>();
		foreach (Transform transform in attributesParent.GetComponentsInChildren<Transform>())
        {
			GameObject attribute=transform.gameObject;
			Text text = attribute.GetComponent<Text>();
			if(text==null)
				continue;
			SetAttribute(attribute.name, 5);
			Debug.Log(attribute.name);
            text.text= attribute.name + ": " + 5;
        }
		ClickedField(Vector2.zero);
		StartingTeamChanged();

            
    }

	public void SetAttribute(string name, int value)
	{
		if(playerAttributes.ContainsKey(name))
		{
			if(playerAttributes[name].maxValue>=value&&value>=playerAttributes[name].minValue)
				playerAttributes[name].SetStartingAttributeValueAndExp(value);
		}
		else
		{
			playerAttributes.Add(name, new Attribute(name, 5));
			SetAttribute(name, value);

		}

	}


    public void IncrementAttribute(Text which)
    {
		if(playerAttributes[which.name].value<playerAttributes[which.name].maxValue&&remainingPoints>0)
        {
			playerAttributes[which.name].IncrementStartingAttributeValue();
            which.text=playerAttributes[which.name].name+": "+playerAttributes[which.name].value;
            remainingPointsText.text="Remaining points: "+ --remainingPoints;
            kick.Play();
        }
        
    }
    public void DecrementAttribute(Text which)
    {
        if (playerAttributes[which.name].value>playerAttributes[which.name].minValue)
        {
			playerAttributes[which.name].DecrementStartingAttributeValue();
            which.text = playerAttributes[which.name].name + ": " + playerAttributes[which.name].value;
            remainingPointsText.text = "Remaining points: " + ++remainingPoints;
            kick.Play();
        }

    }

    public void SaveName(string name)
    {
        playerInfo.playerName = name;
        Debug.Log("Saved name: " + name);
    }
	public void SaveAge(string a)
	{
		int age=int.Parse(a);
		playerInfo.playerAge = age;
		Debug.Log("Saved age: " + age);
	}
    public void SaveSurname(string surname)
    {
        playerInfo.playerSurname = surname;
        Debug.Log("Saved surname: " + surname);
    }
    public void SaveEverything()
    {
		/*
		Team[] teams=new Team[teamsDropdown.options.Count];
		string[] teamNames=teamsDropdown.options.Select(o => o.text).ToArray();
		for (int ii = 0; ii < teamNames.Length; ii++)
			teams[ii]=new Team(teamNames[ii]);*/
		CareerManager.gameInfo.calendar=new LeagueCalendar(new Team[]{teams[0], teams[1]});
		playerInfo.playerAttributes=playerAttributes;
		CareerManager.gameInfo.playerStats=playerInfo.Clone();
        CareerManager.gameInfo.marketValue = CalculationsManager.GetStartingMarketValueByTeam(playerInfo.currentTeam);
		Debug.Log("Saving statistics: ");
		Debug.Log(playerInfo.ToString());
        SaveLoad.SaveGame();
		SceneManager.LoadScene("playerMenu");
    }

	public void ClickedField(Vector2 which)
	{
		UnHighlightEverything();
		HighlightField(which);
		playerInfo.preferredPosition=which;
		selectedPositionText.text="Position selected: "+positionNames[Flatten(which)];
	}

	void HighlightField(Vector2 which)
	{
		int index=Flatten(which);
		fields[index].GetComponent<charCreationField>().Highlight();

	}

	public void UnHighlightEverything()
	{
		foreach(GameObject g in fields)
			g.GetComponent<charCreationField>().UnHighlight();
	}

	int Flatten(Vector2 w)
	{
		return (int)((-w.y+1)*3+w.x+1);
	}
		
	public void StartingTeamChanged()
	{
		foreach(Team t in teams)
			if(t.name.Equals(teamsTextDropdown.text))
				playerInfo.currentTeam=t;
	}

	Team[] GenerateTeamLevels()
	{
		List<Team> teams=new List<Team>();;
		teams.Add(new Team("Man Utd", 5,5,5, new SerializableColor(255,00,00), new SerializableColor(255,255,255)));
		teams.Add(new Team("Man City", 5,5,5, new SerializableColor(00,255,255), new SerializableColor(00,00,00)));
		teams.Add(new Team("Liverpool", 4,5,5,new SerializableColor(255 , 00 , 00 ), new SerializableColor(255 , 255 , 255 )));
        teams.Add(new Team("Chelsea", 5,5,4, new SerializableColor(56 , 89 , 255 ), new SerializableColor(255 , 255 , 255 )));
        teams.Add(new Team("Hull City", 1, 2, 1, new SerializableColor(255 , 189 , 76 ), new SerializableColor(00 , 00 , 00 )));
        teams.Add(new Team("Swansea" , 3, 2, 2, new SerializableColor(255 , 255 , 255 ), new SerializableColor(0, 255, 0)));
        teams.Add(new Team("West Bromwich", 2, 2, 2, new SerializableColor(255, 255, 255), new SerializableColor(0, 0, 128)));
        teams.Add(new Team("Everton", 3, 4, 3, new SerializableColor(0, 0, 128), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Middlesbrough", 3, 3, 3, new SerializableColor(220, 20, 60), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Southampton", 3, 3, 3, new SerializableColor(255, 0, 0), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Stoke", 2, 2, 3, new SerializableColor(255, 69, 0), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Tottenham", 4, 4, 4, new SerializableColor(255, 255, 255), new SerializableColor(0, 0, 0)));
        teams.Add(new Team("Watford", 2, 2, 3, new SerializableColor(255, 255, 0), new SerializableColor(255, 0, 0)));
        teams.Add(new Team("Arsenal", 4, 5, 5, new SerializableColor(255, 0, 0), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Leicester", 4, 4, 4, new SerializableColor(0, 0, 205), new SerializableColor(255, 255, 255)));
        teams.Add(new Team("Sunderland", 3, 2, 3, new SerializableColor(255, 255, 255), new SerializableColor(178, 34, 34)));
        teams.Add(new Team("West Ham", 4, 3, 3, new SerializableColor(0, 191, 255), new SerializableColor(178, 34, 34)));
        teams.Add(new Team("Burnley", 2, 2, 1, new SerializableColor(0, 255, 255), new SerializableColor(139, 0, 0)));
        teams.Add(new Team("Crystal Palace", 1, 1, 2, new SerializableColor(0, 0, 255), new SerializableColor(255, 0, 0)));
        teams.Add(new Team("Bournemouth", 1, 2, 1, new SerializableColor(255, 69, 0), new SerializableColor(255, 0, 0)));
        return teams.ToArray();
	}
}

