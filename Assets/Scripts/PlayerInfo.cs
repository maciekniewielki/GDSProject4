using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    [System.Serializable]
    public class Attribute
    {
        public string name;
        public int value;
        public int maxValue;
        public int minValue;

        override
        public string ToString()
        {
            return "name: " + name.ToString() + "\n" + "value: " + value.ToString() + "\n";
        }
    }

    public string playerName { get; set; }
    public string playerSurname { get; set; }
    public string playerNationality { get; set; }
    public Attribute[] playerAttributes;

    public Attribute[] GetPlayerAttributes()
    {
        return playerAttributes;
    }
    public void SetPlayerAttributes(Attribute[] a)
    {
        playerAttributes = a;
    }

}
