using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PlayerInfo
{//
	//TODO add some more functionality

    public string playerName { get; set; }
    public string playerSurname { get; set; }
    public int playerAge { get; set; }
	public Dictionary<string, Attribute> playerAttributes;
	public Vector2 preferredPosition;
	public string currentTeam;

	public PlayerInfo()
	{
		playerAttributes=new Dictionary<string, Attribute>();
	}

	public PlayerInfo Clone()
	{
		PlayerInfo p=new PlayerInfo();
		p.playerAge=playerAge;
		p.playerName=playerName;
		p.playerSurname=playerSurname;
		p.playerAttributes=playerAttributes;
		p.preferredPosition=preferredPosition;
		p.currentTeam=currentTeam;
		return p;
	}

	public Dictionary<string, Attribute> GetPlayerAttributes()
    {
        return playerAttributes;
    }
	public void SetPlayerAttributes(Dictionary<string, Attribute> a)
    {
        playerAttributes = a;
    }

	public Attribute GetAttribute(string name)
	{
		return playerAttributes[name];
	}

	override
	public string ToString()
	{
		string s="";
		s+="Name: "+playerName+"\n";
		s+="Surname: "+playerSurname+"\n";
		s+="Age: "+playerAge+"\n";
		s+="Attributes: "+"\n";
		foreach(KeyValuePair<string, Attribute> kvp in playerAttributes)
			s+=kvp.Key+": "+kvp.Value+"\n";
		return s;
	}
		

}
