using UnityEngine;
using System.Collections;

public class StatisticsManager : MonoBehaviour 
{
	private MatchStatistics stats;

	public MatchStatistics endStatistics;

	void Awake () 
	{
		GameManager.instance.onTurnEnd+=IncrementPossession;
		GameManager.instance.player.onActionSuccess+=PlayerSuccess;
		GameManager.instance.player.onActionFail+=PlayerFailed;
		GameManager.instance.onMatchEnd+=Save;
		GameManager.instance.onInitVariablesEnd+=CheckStats;
		DontDestroyOnLoad(gameObject);
	}
		

	void CheckStats()
	{
		stats=GameManager.instance.stats;
	}

	void SaveStatistics()
	{
		endStatistics=GameManager.instance.stats;
        endStatistics.playerTeamShots += (int)(endStatistics.playerMoves["Shoot"].y + endStatistics.playerMoves["LongShot"].y);
        endStatistics.playerTeamCorners += (int)endStatistics.playerMoves["Corner"].y;
        endStatistics.playerTeamFreeKicks += (int)endStatistics.playerMoves["FreeKick"].y;
        endStatistics.playerTeamThrowIns += (int)endStatistics.playerMoves["Out"].y;
        endStatistics.playerTeamFouls += GameManager.instance.player.GetFouls();
        endStatistics.playerTeamYellows += GameManager.instance.player.GetYellowCards();
        endStatistics.playerTeamReds += GameManager.instance.player.GetRedCards();
        if (CareerManager.gameInfo.playerStats.currentTeam.name.Equals(CareerManager.gameInfo.nextMatch.leftTeam.name))
			CareerManager.gameInfo.nextMatch.result=new Vector2(endStatistics.playerTeamGoals, endStatistics.enemyTeamGoals);
		else
			CareerManager.gameInfo.nextMatch.result=new Vector2(endStatistics.enemyTeamGoals, endStatistics.playerTeamGoals);

        CareerManager.gameInfo.UpdateCurrentCareerStatistics(endStatistics);
	}

	void Save()
	{
		Invoke("SaveStatistics", 1f);
	}
		
	void PlayerFailed()
	{
		string move=GameManager.instance.player.actionCompleted;
		if(move.Equals("LongOut"))
			move="Out";
		stats.playerMoves[move]=stats.playerMoves[move]+new Vector2(0,1);
	}

	void PlayerSuccess()
	{
		string move=GameManager.instance.player.actionCompleted;
		if(move.Equals("LongOut"))
			move="Out";

		stats.playerMoves[move]=stats.playerMoves[move]+new Vector2(1,1);
	}

	void IncrementPossession()
	{
		if(GameManager.instance.possession==Side.PLAYER)
			stats.playerTeamPossessionTurns++;
		else
			stats.enemyTeamPossessionTurns++;
	}
		
}
