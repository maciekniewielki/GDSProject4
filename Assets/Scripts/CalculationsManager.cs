using UnityEngine;
using System.Collections;

public class CalculationsManager: MonoBehaviour
{
	//TODO make it work
	/*
	private MatchStatistics stats;
	private PlayerInfo playerInfo;

	void Start()
	{
		playerInfo=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerInfo;
		stats=GameManager.stats;
	}

	public Vector2[] GetPositions(string which)
	{
		if(which.Equals("Pass"))
			return GetPassingPositions();
		else
			return GetCrossingPositions();
	}

	public Vector2[] GetCrossingPositions()
	{
		return new Vector2[]{new Vector2(1,0)};
	}
		

	public Vector2[] GetPassingPositions(Vector2 source)
	{
		Vector2 pos = source;
		Vector2[] positions;

		if(pos.x==-1 && pos.y==1)
			positions=new Vector2[]{new Vector2(0, 1), new Vector2(0, 0), new Vector2(-1, 0)};
		else if(pos.x==0 && pos.y==1)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, 1)};
		else if(pos.x==1 & pos.y==1)
			positions=new Vector2[]{new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0)};
		else if(pos.x==-1 && pos.y==0)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 0), new Vector2(-1, -1)};
		else if(pos.x==0 && pos.y==0)
			positions=new Vector2[]{new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)};
		else if(pos.x==1 && pos.y==0)
			positions=new Vector2[]{new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, -1)};
		else if(pos.x==-1 && pos.y==-1)
			positions=new Vector2[]{new Vector2(-1, 0), new Vector2(0, 0), new Vector2(0, -1)};
		else if(pos.x==0 && pos.y==-1)
			positions=new Vector2[]{new Vector2(-1, -1), new Vector2(0, 0), new Vector2(1, -1)};
		else
			positions=new Vector2[]{new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, -1)};

		return positions;
	}

	public Vector2[] GetAttackingPositions(Vector2 source)
	{
		Vector2 pos = source;
		Vector2[] positions;
		if (pos.x == 1)
			return null;
		else
		{
			pos.x++;
			if (pos.y == 1)
				positions=new Vector2[]{new Vector2(pos.x, pos.y), new Vector2(pos.x, 0)};
			else if (pos.y == -1)
				positions=new Vector2[]{new Vector2(pos.x, pos.y), new Vector2(pos.x, 0)};
			else
				positions=new Vector2[]{new Vector2(pos.x, 1), new Vector2(pos.x, 0), new Vector2(pos.x, -1)};
		}
		return positions;
	}

	public Vector2 GetRandomAttackingPosition(Vector2 source, Side currentPossession)
	{
		source.x *= currentPossession == Side.PLAYER ? 1 : -1;

		if (source.x== 1)
			return new Vector2(currentPossession, 0);

		Vector2[] passingPositions=GetPassingPositions();


		while(true)
		{
			int randIndex=Random.Range(0,passingPositions.Length);
			if(passingPositions[randIndex].x>source.x&&currentPossession==Side.PLAYER)
				return passingPositions[randIndex];
			else if(passingPositions[randIndex].x<source.x&&currentPossession==Side.ENEMY)
				return passingPositions[randIndex];

		}

	}

	public int GetFormationPointsInPosition(Vector2 pos, Side side)
	{
		if(side==Side.ENEMY)
		{
			if(pos.x==-1)
				return stats.enemyTeam.attack;
			else if(pos.x==0)
				return stats.enemyTeam.midfield;
			else
				return stats.enemyTeam.defence;
		}

		else
		{
			if(pos.x==-1)
				return stats.playerTeam.defence;
			else if(pos.x==0)
				return stats.playerTeam.midfield;
			else
				return stats.playerTeam.attack;
		}
	}

	public bool IsMoveSuccessful(string name, int value, Vector2 source, Vector2 destination)
	{
		
	}

	public int CalculatePassingScoreOnField(Vector2 source, Vector2 destination)
	{
		int yourPercent=playerPassing*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(moveDestination)*20;
		SetCurrentBallPosition(moveDestination);
		if(yourPercent>=enemyPercent)
		{
			AddText("Pass successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.passesSuccessful++;
		}
		else
		{
			AddText("Pass unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.passesUnsuccessfull++;
			ChangePossession();
		}
	}

*/
}
