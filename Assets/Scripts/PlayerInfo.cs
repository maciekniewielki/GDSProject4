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
    public string playerNationality { get; set; }
	public Dictionary<string, Attribute> playerAttributes;

	public PlayerInfo()
	{
		playerAttributes=new Dictionary<string, Attribute>();
	}

	public Dictionary<string, Attribute> GetPlayerAttributes()
    {
        return playerAttributes;
    }
	public void SetPlayerAttributes(Dictionary<string, Attribute> a)
    {
        playerAttributes = a;
    }
		

}
