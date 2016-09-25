using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeasonStatisticsViewer : MonoBehaviour 
{
	public Text leagueTableDisplay;

	void Start () 
	{
		CareerManager.gameInfo.calendar.AddPointsForWeek(CareerManager.gameInfo.calendar.weeks.Length);
		leagueTableDisplay.text = CareerManager.gameInfo.calendar.ConvertToLeagueTableString();
	}
	
	public void ResetLeague()
	{
		CareerManager.gameInfo.calendar = new LeagueCalendar(CareerManager.gameInfo.calendar.teams);
		CareerManager.gameInfo.currentSeason++;
		CareerManager.gameInfo.currentRound = 1;
		CareerManager.gameInfo.currentWeekDay = 0;
        CareerManager.gameInfo.allCareerStatistics.Add(new CareerStatistics());
		LeaveToPlayerMenu();
	}

	void LeaveToPlayerMenu()
	{
		SceneManager.LoadScene("playerMenu");
	}
}

