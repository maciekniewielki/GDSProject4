using UnityEngine;
using System.Collections;

public class MatchWeek
{
	public int weekNumber;
	public MatchResultContainer[] matches;
	public bool addedPoints;

	public MatchWeek(Team[] teams, int weekNumber=1)
	{
		addedPoints=false;
		this.weekNumber=weekNumber;
		matches=new MatchResultContainer[teams.Length/2];
		for (int ii = 0; ii < teams.Length/2; ii++)
		{
			matches[ii]=new MatchResultContainer(teams[ii], teams[teams.Length-ii-1]);
		}
	}

	override
	public string ToString()
	{
		string s="";
		s+="Week: "+weekNumber+"\n";
		foreach(MatchResultContainer c in matches)
			s+=c.ToString();
		return s;
	}

	public void ReverseMatchSides()
	{
		foreach(MatchResultContainer m in matches)
			m.ReverseSides();
	}

	public void PlayAIMatches(string playerTeamName)
	{
		foreach(MatchResultContainer mrc in matches)
			if(!mrc.ContainsTeamName(playerTeamName))
				mrc.GenerateResult();
	}

	public MatchResultContainer GetPlayerMatch(string playerTeamName)
	{
		foreach(MatchResultContainer mrc in matches)
		{
			Debug.Log("Sprawdzam ."+mrc.leftTeam.name+". i ."+mrc.rightTeam.name+". z druzyna gracza: ."+playerTeamName+".");
			if(mrc.ContainsTeamName(playerTeamName))
				return mrc;
		}
		return null;
	}

	public void AddPointsForLeague()
	{
		if(addedPoints)
			return;
		foreach(MatchResultContainer m in matches)
			m.AddPointsForMatch();
		addedPoints=true;
	}
		
}
