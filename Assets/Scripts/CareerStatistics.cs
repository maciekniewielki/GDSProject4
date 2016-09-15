using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CareerStatistics
{
    public int matchesPlayed;
    public Dictionary<string, SerializableVector2> playerMoves { get; set; }
    public int playerGoals { get; set; }
    public int playerYellows;
    public int playerReds;
    public int playerFouls;
    public int playerTurnsOnPitch;
    public string[] possiblePlayerMoves;

    public CareerStatistics()
    {
        this.matchesPlayed = 0;
        this.possiblePlayerMoves = new string[] { "Pass", "Dribble", "Tackle", "FinishHead", "Shoot", "LongShot", "Cross", "Corner", "Out", "Head", "FreeKick", "Penalty" };
        this.playerMoves = new Dictionary<string, SerializableVector2>();
        foreach (string s in this.possiblePlayerMoves)
            this.playerMoves.Add(s, new Vector2(0, 0));
    }

    public void UpdateInformation(MatchStatistics m)
    {
        foreach (KeyValuePair<string, Vector2> kvp in m.playerMoves)
            playerMoves[kvp.Key] =playerMoves[kvp.Key]+ m.playerMoves[kvp.Key];
        matchesPlayed++;
        playerGoals += m.playerGoals;
        playerYellows += m.playerYellows;
        playerReds += m.playerReds;
        playerFouls += m.playerFouls;
        playerTurnsOnPitch += m.playerTurnsOnPitch;
    }
}
