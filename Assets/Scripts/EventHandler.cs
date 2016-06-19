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
        playerAttributes = player.playerInfo.GetPlayerAttributes();
		foreach (KeyValuePair<string, Attribute> pair in playerAttributes)
        {
			Attribute a=pair.Value;
            Text text = attributesParent.transform.FindChild(a.name).GetComponent<Text>();
            text.text= a.name + ": " + a.value;
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
    }
}
