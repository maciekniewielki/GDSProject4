using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {

	public GameObject[] fields;
	public string[] positionNames=new string[]{"LB", "LM", "LF", "CB", "CM", "CF", "RB", "RM", "RF"};
	public Text selectedPositionText;
    public Text remainingPointsText;
    public GameObject attributesParent;
	private Dictionary<string, Attribute> playerAttributes;
    private int remainingPoints = 20;
	private PlayerInfo playerInfo;

	void Awake()
	{
		playerInfo=new PlayerInfo();
		CareerManager.gameInfo=new GameInformation();
	}

	void Start ()
    {
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
}
