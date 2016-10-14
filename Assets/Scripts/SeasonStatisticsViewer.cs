using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeasonStatisticsViewer : MonoBehaviour 
{
	public Text leagueTableDisplay;
    public Text nextSeasonButtonText;

	void Start () 
	{
        CareerManager.gameInfo.playerStats.playerAge++;
        if (CareerManager.gameInfo.playerStats.playerAge == 35)
            nextSeasonButtonText.text = "End Game";
        CareerManager.gameInfo.calendar.AddPointsForWeek(CareerManager.gameInfo.calendar.weeks.Length);
		leagueTableDisplay.text = CareerManager.gameInfo.calendar.ConvertToLeagueTableString();
	}
	
	public void ResetLeague()
	{
        if (CareerManager.gameInfo.playerStats.playerAge == 35)
        {
            LeaveToMainMenu();
            return;
        }

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

    void LeaveToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}

