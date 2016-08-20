using UnityEngine;
using System.Collections;

public class LeagueCalendar
{
	public string leagueName;
	public MatchWeek[] weeks;

	public LeagueCalendar(Team []teams)
	{
		weeks=new MatchWeek[teams.Length*2-2];
		MatchWeek[] firstMatches=GenerateWeeks(teams);
		MatchWeek[] secondLegMatches=GenerateWeeks(teams);
		foreach(MatchWeek w in secondLegMatches)
		{
			w.ReverseMatchSides();
			w.weekNumber+=teams.Length-1;
		}
		for (int ii = 0; ii < teams.Length-1; ii++)
		{
			weeks[ii]=firstMatches[ii];
			weeks[ii+teams.Length-1]=secondLegMatches[ii];
		}
	}

	MatchWeek[] GenerateWeeks(Team []teams)
	{
		Team[] teamList=(Team[])teams.Clone();
		int len=teamList.Length;
		MatchWeek[] weeks=new MatchWeek[len-1];
		for (int ii=0; ii<len-1;ii++)
		{
			weeks[ii]=new MatchWeek(teamList, ii+1);
			ShuffleMatches(weeks[ii].matches);
			DoOneCycle(teamList);
		}

		return weeks;
	}



	void DoOneCycle(Team[] teams)
	{
		Team temp=teams[1];
		for (int ii = teams.Length-1; ii >= 2; ii--)
		{
			int newIndex=ii==teams.Length-1?1: ii+1; 
			teams[newIndex]=teams[ii];
		}
		teams[2]=temp;
	}

	void ShuffleMatches(MatchResultContainer[] matches)
	{
		for (int t = 0; t < matches.Length; t++ )
		{
			MatchResultContainer tmp = matches[t];
			int r = Random.Range(t, matches.Length);
			matches[t] = matches[r];
			matches[r] = tmp;
		}
	}

	public string MatchesToString(MatchResultContainer[] matches)
	{
		string s="";
		foreach(MatchResultContainer match in matches)
			s+=match.leftTeam.name+" - "+match.rightTeam.name+"\n";
		return s;
	}

	override
	public string ToString()
	{
		string s="";
		foreach(MatchWeek week in weeks)
			s+=week.ToString();
		return s;
	}

	public MatchWeek GetWeekByNumber(int index)
	{
		return weeks[index-1];
	}

	public void CalculateScoreForWeek(int which, string playerTeamName)
	{
		weeks[which-1].PlayAIMatches(playerTeamName);
	}


}
