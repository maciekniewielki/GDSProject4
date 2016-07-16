using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsViewer : MonoBehaviour 
{
	public Text playerTeamNameDisplay;
	public Text enemyTeamNameDisplay;
	public Text score;

	void Start()
	{
		MatchStatistics stats= GameObject.Find("MatchStats").GetComponent<StatisticsManager>().endStatistics;

		/*
		Team playerTeam=new Team("Real Madrid", 1,1,1);
		Team enemyTeam=new Team("Chelsea", 1,1,1);
		MatchStatistics stats=new MatchStatistics(playerTeam, enemyTeam);
		stats.playerTeamGoals=2;
		stats.enemyTeamGoals=1;
		*/

		ViewStatistics(stats);
	}

	public void ViewStatistics(MatchStatistics stats)
	{
		playerTeamNameDisplay.text=stats.playerTeam.name;
		enemyTeamNameDisplay.text=stats.enemyTeam.name;
		score.text=stats.playerTeamGoals+":"+stats.enemyTeamGoals;
	}

}
