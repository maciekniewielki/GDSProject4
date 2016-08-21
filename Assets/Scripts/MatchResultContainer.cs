using UnityEngine;
using System.Collections;

public class MatchResultContainer
{
	public Team leftTeam;
	public Team rightTeam;
	public Vector2 result;

	public MatchResultContainer(Team leftTeam, Team rightTeam)
	{
		this.leftTeam=leftTeam;
		this.rightTeam=rightTeam;
	}
	public void GenerateResult()
	{
		result=CalculationsManager.GetMatchResultByTeams(leftTeam, rightTeam);
	}

	override
	public string ToString()
	{
		string s="";
		if(result!=null)
			s+=leftTeam.name+" "+result.x+":"+result.y+" "+rightTeam.name;
		else
			s+=leftTeam.name+" : "+rightTeam.name;
		return s+"\n";
	}

	public void ReverseSides()
	{
		Team tmp=leftTeam;
		leftTeam=rightTeam;
		rightTeam=tmp;
	}

	public bool ContainsTeamName(string teamName)
	{
		return leftTeam.name.Equals(teamName)||rightTeam.name.Equals(teamName);
	}

	public void AddPointsForMatch()
	{
		if(result.x>result.y)
			leftTeam.AddPoints(3);
		else if(result.y>result.x)
			rightTeam.AddPoints(3);
		else
		{
			leftTeam.AddPoints(1);
			rightTeam.AddPoints(1);
		}
	}

}
