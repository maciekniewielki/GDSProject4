using UnityEngine;
using System.Collections;

public class LeagueCalendar
{
	public string leagueName;
	public MatchWeek[] weeks;

	public LeagueCalendar(Team []teams)
	{
		MatchResultContainer[] matches=new MatchResultContainer[teams.Length*teams.Length-teams.Length];
		weeks=new MatchWeek[teams.Length*2-2];
		int iterator=0;
		foreach(Team l in teams)
			foreach(Team r in teams)
			{
				if(l.Equals(r))
					continue;
				matches[iterator]=new MatchResultContainer(l, r);
				iterator++;
			}
		Debug.Log("Before shuffle: \n"+MatchesToString(matches));
		ShuffleMatches(matches);
		Debug.Log("After shuffle: \n"+MatchesToString(matches));
		/*
		int matchIter=0;
		int weekIter=-1;
		for(;matchIter<matches.Length;matchIter++)
		{
			if(matchIter%teams.Length==0)
			{
				weeks[weekIter].matches=new MatchResultContainer[teams.Length/2];
				weekIter++;
			}
			
			
		}*/

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
			foreach(MatchResultContainer container in week.matches)
				s+=container.leftTeam.name+" - "+container.rightTeam.name+"\n";
		return s;
	}
}
