using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    public PlayerInfo player;
    public Text remainingPointsText;
    public GameObject attributesParent;
    private PlayerInfo.Attribute[] playerAttributes;
    private int remainingPoints = 20;

	void Start ()
    {
        playerAttributes = player.GetPlayerAttributes();
        foreach (PlayerInfo.Attribute a in playerAttributes)
        {
            Text text = attributesParent.transform.FindChild(a.name).GetComponent<Text>();
            text.text= a.name + ": " + a.value;
        }
            
    }
	

    public void IncrementAttribute(Text which)
    {
        foreach(PlayerInfo.Attribute a in playerAttributes)
            if(a.name==which.name&&a.value<a.maxValue&&remainingPoints>0)
            {
                which.text=a.name+": "+ ++a.value;
                remainingPointsText.text="Remaining points: "+ --remainingPoints;
                
            }
        
    }
    public void DecrementAttribute(Text which)
    {
        foreach (PlayerInfo.Attribute a in playerAttributes)
            if (a.name == which.name&&a.value>a.minValue)
            {
                which.text = a.name + ": " + --a.value;
                remainingPointsText.text = "Remaining points: " + ++remainingPoints;
            }

    }

    public void SaveName(string name)
    {
        player.playerName = name;
        Debug.Log("Saved name: " + name);
    }
    public void SaveSurname(string surname)
    {
        player.playerSurname = surname;
        Debug.Log("Saved surname: " + surname);
    }
    public void SaveEverything()
    {
        Debug.Log("To kiedys bedzie cos robic");
    }
}
