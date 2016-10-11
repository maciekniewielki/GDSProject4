using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class GameInformation 
{
	public bool wentToIndividualTraining;
	public bool wentToClubTraining;
	public int currentSeason;
	public int currentRound;
	public int currentWeekDay;
    public int marketValue;
    public PlayerInfo playerStats;
	public LeagueCalendar calendar;
	public MatchResultContainer nextMatch;
    public List<CareerStatistics> allCareerStatistics;

    public CareerStatistics currentCareerStatistics
    {
        get
        {
            return allCareerStatistics.Last();
        }
    }


	public GameInformation (int currentSeason=1, int currentRound=1, int currentWeekDay=0, PlayerInfo playerStats=default(PlayerInfo), bool wentToIndividualTraining=false, bool wentToClubTraining=false)
	{
		this.wentToIndividualTraining = wentToIndividualTraining;
		this.wentToClubTraining = wentToClubTraining;
		this.currentSeason = currentSeason;
		this.currentRound = currentRound;
		this.currentWeekDay = currentWeekDay;
		this.playerStats = playerStats;
        this.allCareerStatistics = new List<CareerStatistics>();
        this.allCareerStatistics.Add(new CareerStatistics());
	}

    public void UpdateCurrentCareerStatistics(MatchStatistics stats)
    {
        allCareerStatistics.Last().UpdateInformation(stats);
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

    public Table ToDataTable()
    {
        Table data = new Table(19, currentSeason+1);
        string[] headers = new string[]
        {
            "Season",
            "Club",
            "League",
            "Minutes",
            "Matches",
            "Goals",
            "Shots",
            "Long Shots",
            "Passes",
            "Dribbles",
            "Tackles",
            "Corners",
            "Free Kicks",
            "Throw-ins",
            "Headers",
            "Fouls",
            "Yellow Cards",
            "Red Cards",
            "Avg. Rating"
        };
        data.SetHeader(headers);
        int count = allCareerStatistics.Count;
        int seasonCount = 0;
        foreach (CareerStatistics c in allCareerStatistics)
        {
            seasonCount++;
            data.SetRow(count, c.ToTableRow(seasonCount, playerStats.currentTeam.name, "English"));
            count--;
        }
        Debug.Log(data);

        return data;
        
    }
}
