using UnityEngine;

[System.Serializable]
public class Team
{
    public string name {get; set; }
    public int defence { get; set; }
    public int midfield { get; set; }
    public int attack { get; set; }
	public int defenceYellowCards { get; set; }
	public int midfieldYellowCards { get; set; }
	public int attackYellowCards { get; set; }
	public int pointsInLeague;
	public SerializableColor bgColor;
	public SerializableColor textColor;

	public Team(string name, int defence=1, int midfield=1, int attack=1, Color bgColor=default(Color), Color textColor=default(Color))
    {
		this.pointsInLeague=0;
        this.name = name;
        this.defence = defence;
        this.midfield = midfield;
        this.attack = attack;
		this.defenceYellowCards=0;
		this.midfieldYellowCards=0;
		this.attackYellowCards=0;
		this.bgColor=bgColor;
		this.textColor=textColor;
    }

	public void AddPoints(int points)
	{
		pointsInLeague+=points;
	}

	public void ResetPoints()
	{
		pointsInLeague=0;
	}

    public int GetAddedAttributes()
    {
        return attack + midfield + defence;
    }
}
