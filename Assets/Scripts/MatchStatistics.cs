
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MatchStatistics
{
    public Dictionary<Vector2,int> fieldBallCount { get; set; }
	public int playerGoals {get; set;}
    public int playerTeamGoals { get; set; }
    public int enemyTeamGoals { get; set; }
    public int playerTeamShots { get; set; }
    public int enemyTeamShots { get; set; }
	public int tacklesSuccessful {get; set;}
	public int tacklesUnsuccessfull {get; set;}
	public int passesSuccessful {get; set;}
	public int passesUnsuccessfull {get; set;}
	public int crossesSuccessful {get; set;}
	public int crossesUnsuccessfull {get; set;}
	public Team playerTeam;
	public Team enemyTeam;

	public MatchStatistics(Team playerTeam, Team enemyTeam)
    {
        this.fieldBallCount = new Dictionary<Vector2, int>();
        this.playerTeamGoals = 0;
        this.enemyTeamGoals = 0;
        this.playerTeamShots = 0;
        this.enemyTeamShots = 0;
		this.passesSuccessful=0;
		this.passesUnsuccessfull=0;
		this.tacklesSuccessful=0;
		this.tacklesUnsuccessfull=0;
		this.crossesSuccessful=0;
		this.crossesUnsuccessfull=0;
		this.playerTeam=playerTeam;
		this.enemyTeam=enemyTeam;
    }

    override
    public string ToString()
    {
        string s = "";
        s += "Field Ball Possession:\n";
        foreach (KeyValuePair<Vector2, int> kvp in fieldBallCount)
        {
            s += string.Format("Position = {0}, count = {1}\n", kvp.Key, kvp.Value);
        }
        s += "Player Team shots= " + playerTeamShots + ". Goals= " + playerTeamGoals + "\n";
        s += "Enemy Team shots= " + enemyTeamShots + ". Goals= " + enemyTeamGoals + "\n";
		s += "Tackles: successful- " + tacklesSuccessful + ". Unsuccessful- " + tacklesUnsuccessfull +"\n";
		s += "Passes: successful- " + passesSuccessful + ". Unsuccessful- " + passesUnsuccessfull +"\n";
		s += "Crosses: successful- " + crossesSuccessful + ". Unsuccessful- " + crossesUnsuccessfull +"\n";
        return s;
    }

	public void AddGoal(Side side, bool playerGoal)
	{
		if(side==Side.PLAYER)
		{
			if(playerGoal)
				playerGoals++;
			playerTeamShots++;
			playerTeamGoals++;
		}
		else
		{
			enemyTeamGoals++;
			enemyTeamShots++;
		}
	}

}

public enum Side
{
	PLAYER, ENEMY
}