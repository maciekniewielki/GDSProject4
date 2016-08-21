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
            
        }
        
    }
    public void DecrementAttribute(Text which)
    {
        if (playerAttributes[which.name].value>playerAttributes[which.name].minValue)
        {
			playerAttributes[which.name].DecrementStartingAttributeValue();
            which.text = playerAttributes[which.name].name + ": " + playerAttributes[which.name].value;
            remainingPointsText.text = "Remaining points: " + ++remainingPoints;
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
		CareerManager.gameInfo.calendar=new LeagueCalendar(teams);
		playerInfo.playerAttributes=playerAttributes;
		CareerManager.gameInfo.playerStats=playerInfo.Clone();
		Debug.Log("Saving statistics: ");
		Debug.Log(playerInfo.ToString());
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
		Debug.Log("Highlighting " + which);
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
		teams.Add(new Team("Manchester United", 1,1,1, new Color(0xff/255f,0x00/255f,0x00/255f), new Color(0xff/255f,0xff/255f,0xff/255f)));
		teams.Add(new Team("Manchester City", 1,1,1, new Color(0x00/255f,0xff/255f,0xff/255f), new Color(0x00/255f,0x00/255f,0x00/255f)));
		//teams.Add(new Team("Jeszcze jedna druzyna", 1,1,1));
		//teams.Add(new Team("I jeszcze jedna", 1,1,1));
		return teams.ToArray();
	}
}

