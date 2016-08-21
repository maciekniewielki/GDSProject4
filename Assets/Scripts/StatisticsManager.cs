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
		if(CareerManager.gameInfo.playerStats.currentTeamName.Equals(CareerManager.gameInfo.nextMatch.leftTeam.name))
			CareerManager.gameInfo.nextMatch.result=new Vector2(endStatistics.playerTeamGoals, endStatistics.enemyTeamGoals);
		else
			CareerManager.gameInfo.nextMatch.result=new Vector2(endStatistics.enemyTeamGoals, endStatistics.playerTeamGoals);
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
