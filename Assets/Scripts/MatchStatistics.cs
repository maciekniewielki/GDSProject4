
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class MatchStatistics
{
    public Dictionary<Vector2,int> fieldBallCount { get; set; }
	public int playerGoals {get; set;}
    public int playerTeamGoals { get; set; }
    public int enemyTeamGoals { get; set; }
    public int playerTeamShots { get; set; }
    public int enemyTeamShots { get; set; }
	public int enemyTeamPossessionTurns;
	public int playerTeamPossessionTurns;
	public Team playerTeam;
	public Team enemyTeam;
	public Dictionary<string, Vector2> playerMoves { get; set; }

	private string[] possiblePlayerMoves;

	public MatchStatistics(Team playerTeam, Team enemyTeam)
    {
		this.possiblePlayerMoves=new string[]{"Pass", "Dribble", "Tackle", "FinishHead", "Shoot", "LongShoot", "Cross", "Corner", "Out", "Head", "FreeKick", "Penalty"};
        this.fieldBallCount = new Dictionary<Vector2, int>();
		this.playerMoves=new Dictionary<string, Vector2>();
		foreach(string s in this.possiblePlayerMoves)
			this.playerMoves.Add(s, new Vector2(0,0));
			
        this.playerTeamGoals = 0;
        this.enemyTeamGoals = 0;
        this.playerTeamShots = 0;
        this.enemyTeamShots = 0;
		this.playerTeamPossessionTurns=0;
		this.enemyTeamPossessionTurns=0;
		this.playerTeam=playerTeam;
		this.enemyTeam=enemyTeam;
    }

    override
    public string ToString()
    {
        string s = "";
        s += "Player Team shots= " + playerTeamShots + ". Goals= " + playerTeamGoals + "\n";
        s += "Enemy Team shots= " + enemyTeamShots + ". Goals= " + enemyTeamGoals + "\n";
		if(this.playerMoves==null)
			Debug.Log("Kurwa");
		foreach(KeyValuePair<string, Vector2> pair in this.playerMoves)
		{
			s+=string.Format("Skill = {0}, successful = {1}, total = {2}", pair.Key, pair.Value.x, pair.Value.y);
		}
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