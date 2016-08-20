using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameInformation 
{
	public bool wentToIndividualTraining;
	public bool wentToClubTraining;
	public int currentRound;
	public int currentWeekDay;
	public PlayerInfo playerStats;
	public LeagueCalendar calendar;
	public MatchResultContainer nextMatch;


	public GameInformation (int currentRound=1, int currentWeekDay=0, PlayerInfo playerStats=default(PlayerInfo), bool wentToIndividualTraining=false, bool wentToClubTraining=false)
	{
		this.wentToIndividualTraining = wentToIndividualTraining;
		this.wentToClubTraining = wentToClubTraining;
		this.currentRound = currentRound;
		this.currentWeekDay = currentWeekDay;
		this.playerStats = playerStats;
	}

	override
	public string ToString()
	{
		string s="";
		s+="Current round: "+ currentRound+"\n";
		s+="Current day: "+ currentWeekDay+"\n";
		s+="Went to Individual Training: "+ wentToIndividualTraining+"\n";
		s+="Went to club Training"+ wentToClubTraining+"\n";

		return s;
	}
}
