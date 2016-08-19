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
}
