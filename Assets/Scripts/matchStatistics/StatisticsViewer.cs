using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsViewer : MonoBehaviour 
{
	public Text playerTeamNameDisplay;
	public Text enemyTeamNameDisplay;
	public Text score;
    public Text playerTeamShotsDisplay;
    public Text TeamNames;
    public Text ShotsSign;
    public Text PlayerName;
    public Text PlayerGoals;
    public Text PlayerPassess;

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
        playerTeamShotsDisplay.text = stats.playerTeamShots.ToString()+" - "+stats.enemyTeamShots.ToString();
        TeamNames.text = stats.playerTeam.name + " - " + stats.enemyTeam.name;
        PlayerGoals.text = "Goals: "+stats.playerGoals.ToString();
        
    }

}
