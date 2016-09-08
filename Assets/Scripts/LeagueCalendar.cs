using UnityEngine;
using System.Collections;
using System.Linq;
[System.Serializable]
public class LeagueCalendar
{
	public string leagueName;
	public MatchWeek[] weeks;
	public Team[] teams;

	public LeagueCalendar(Team []teams)
	{
		this.teams=teams;
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
			if(ii!=len-2)
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

	public void AddPointsForWeek(int week)
	{
		weeks[week-1].AddPointsForLeague();
	}

	public string ConvertToLeagueTableString()
	{
		System.Array.Sort(teams, delegate(Team x, Team y) {
			return y.pointsInLeague-x.pointsInLeague;
		});

		int[] nameLengths= teams.Select(t => t.name.Length).ToArray();
		int maxNameLength= nameLengths.Max();

		string s="";
		for (int ii = 0; ii < teams.Length; ii++)
		{
			s+=(ii+1)+". ";
			if(ii<=8)
				s+=" ";
			s+=teams[ii].name;
			for (int jj = 0; jj < maxNameLength-teams[ii].name.Length+1; jj++)
				s+=" ";
			s+=teams[ii].pointsInLeague+"\n";
		}
			
			
		return s;
	}
}
