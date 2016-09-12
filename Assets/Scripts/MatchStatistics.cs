
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
	public int enemyTeamPossessionTurns;
	public int playerTeamPossessionTurns;
	public Team playerTeam;
	public Team enemyTeam;
	public Dictionary<string, Vector2> playerMoves { get; set; }
	public int playerTeamYellows;
	public int enemyTeamYellows;
	public int playerTeamReds;
	public int enemyTeamReds;
	public int playerTeamCorners;
	public int enemyTeamCorners;
	public int playerTeamFreeKicks;
	public int enemyTeamFreeKicks;
	public int playerTeamThrowIns;
	public int enemyTeamThrowIns;
	public int enemyTeamFouls;
	public int playerTeamFouls;

	public int playerYellows;
	public int playerReds;
	public int playerFouls;
    public int playerTurnsOnPitch;

	public string[] possiblePlayerMoves;

	public MatchStatistics(Team playerTeam, Team enemyTeam)
    {
		this.possiblePlayerMoves=new string[]{"Pass", "Dribble", "Tackle", "FinishHead", "Shoot", "LongShot", "Cross", "Corner", "Out", "Head", "FreeKick", "Penalty"};
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
        this.playerTurnsOnPitch = 90;
    }

    override
    public string ToString()
    {
        string s = "";
        s += "Player Team shots= " + playerTeamShots + ". Goals= " + playerTeamGoals + "\n";
        s += "Enemy Team shots= " + enemyTeamShots + ". Goals= " + enemyTeamGoals + "\n";
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

	public void AddMiss(Side side, bool playerGoal)
	{
		if(side==Side.PLAYER)
			playerTeamShots++;
		else
			enemyTeamShots++;
	}

	public void AddFoul(Side side)
	{
		if(side==Side.ENEMY)
			enemyTeamFouls++;
		else
			playerTeamFouls++;
	}

	public void AddSetPiece(Side side, RestartActionType type)
	{
		if(type==RestartActionType.CORNER&&side==Side.ENEMY)
			enemyTeamCorners++;
		else if(type==RestartActionType.FREEKICK&&side==Side.ENEMY)
			enemyTeamFreeKicks++;
		else if(type==RestartActionType.OUT&&side==Side.ENEMY)
			enemyTeamThrowIns++;
		else if(type==RestartActionType.CORNER&&side==Side.PLAYER)
			playerTeamCorners++;
		else if(type==RestartActionType.FREEKICK&&side==Side.PLAYER)
			playerTeamFreeKicks++;
		else if(type==RestartActionType.OUT&&side==Side.PLAYER)
			playerTeamThrowIns++;
	}

	public void AddCard(Side side, string card)
	{
		if(side==Side.PLAYER&&card.Equals("yellow"))
			playerTeamYellows++;
		else if(side==Side.PLAYER&&card.Equals("red"))
			playerTeamReds++;
		else if(side==Side.ENEMY&&card.Equals("red"))
			enemyTeamReds++;
		else if(side==Side.ENEMY&&card.Equals("yellow"))
			enemyTeamYellows++;
	}

}



public enum Side
{
	PLAYER, ENEMY
}