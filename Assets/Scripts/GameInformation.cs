using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameInformation 
{
	public bool wentToIndividualTraining;
	public bool wentToClubTraining;
	public int currentSeason;
	public int currentRound;
	public int currentWeekDay;
	public PlayerInfo playerStats;
	public LeagueCalendar calendar;
	public MatchResultContainer nextMatch;
    public CareerStatistics careerStatistics;


	public GameInformation (int currentSeason=1, int currentRound=1, int currentWeekDay=0, PlayerInfo playerStats=default(PlayerInfo), bool wentToIndividualTraining=false, bool wentToClubTraining=false)
	{
		this.wentToIndividualTraining = wentToIndividualTraining;
		this.wentToClubTraining = wentToClubTraining;
		this.currentSeason = currentSeason;
		this.currentRound = currentRound;
		this.currentWeekDay = currentWeekDay;
		this.playerStats = playerStats;
        this.careerStatistics = new CareerStatistics();
	}

	override
	public string ToString()
	{
		string s="";
		s+="Current Season: "+currentSeason+"\n";
		s+="Current round: "+ currentRound+"\n";
		s+="Current day: "+ currentWeekDay+"\n";
		s+="Went to Individual Training: "+ wentToIndividualTraining+"\n";
		s+="Went to club Training"+ wentToClubTraining+"\n";

		return s;
	}
}
