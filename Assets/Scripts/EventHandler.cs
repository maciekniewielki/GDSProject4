using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour {
	//TODO fix everything in character screen creation(fix the dictionary)
    public Player player;
    public Text remainingPointsText;
    public GameObject attributesParent;
	private Dictionary<string, Attribute> playerAttributes;
    private int remainingPoints = 20;

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
            
    }

	public void SetAttribute(string name, int value)
	{
		if(playerAttributes.ContainsKey(name))
		{
			if(playerAttributes[name].maxValue>=value&&value>=playerAttributes[name].minValue)
				playerAttributes[name].value=value;
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
            which.text=playerAttributes[which.name].name+": "+ ++playerAttributes[which.name].value;
            remainingPointsText.text="Remaining points: "+ --remainingPoints;
            
        }
        
    }
    public void DecrementAttribute(Text which)
    {
        if (playerAttributes[which.name].value>playerAttributes[which.name].minValue)
        {
            which.text = playerAttributes[which.name].name + ": " + --playerAttributes[which.name].value;
            remainingPointsText.text = "Remaining points: " + ++remainingPoints;
        }

    }

    public void SaveName(string name)
    {
        player.playerInfo.playerName = name;
        Debug.Log("Saved name: " + name);
    }
    public void SaveSurname(string surname)
    {
        player.playerInfo.playerSurname = surname;
        Debug.Log("Saved surname: " + surname);
    }
    public void SaveEverything()
    {
        Debug.Log("To kiedys bedzie cos robic");

		SceneSwitcher.instance.SwitchSceneTo("playerMenu");
    }
}
