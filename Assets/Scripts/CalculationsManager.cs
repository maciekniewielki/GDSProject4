using UnityEngine;
using System.Collections;

public class CalculationsManager: MonoBehaviour
{
	//TODO make it work

	private MatchStatistics stats;
	private Player player;

	void Start()
	{

	}

	public static Vector2[] GetPositions(string which, Vector2 where)
	{
		if(which.Equals("Pass")||which.Equals("Dribble"))
			return GetPassingPositions(where);
		else
			return GetCrossingPositions(where);
	}

	public static Vector2[] GetCrossingPositions(Vector2 where)
	{
		return new Vector2[]{new Vector2(1,0)};
	}
		

	public static Vector2[] GetPassingPositions(Vector2 source)
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

	public static Vector2[] GetAttackingPositions(Vector2 source, Side side)
	{
		Vector2 pos = source;
		Vector2[] positions;

		source.x *= side == Side.PLAYER ? 1 : -1;
		if(pos.x==-1 && pos.y==1)
			positions=new Vector2[]{new Vector2(0, 1), new Vector2(0, 0)};
		else if(pos.x==0 && pos.y==1)
			positions=new Vector2[]{new Vector2(0, 0), new Vector2(1, 1)};
		else if(pos.x==1 & pos.y==1)
			positions=new Vector2[]{new Vector2(1, 0)};
		else if(pos.x==-1 && pos.y==0)
			positions=new Vector2[]{new Vector2(0, 0)};
		else if(pos.x==0 && pos.y==0)
			positions=new Vector2[]{ new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1)};
		else if(pos.x==1 && pos.y==0)
			positions=null;
		else if(pos.x==-1 && pos.y==-1)
			positions=new Vector2[]{new Vector2(0, 0), new Vector2(0, -1)};
		else if(pos.x==0 && pos.y==-1)
			positions=new Vector2[]{new Vector2(0, 0), new Vector2(1, -1)};
		else
			positions=new Vector2[]{new Vector2(1, 0)};

		Vector2[] normalized=new Vector2[positions.Length];
		if(side==Side.ENEMY)
		{
			for(int ii=0; ii<positions.Length;ii++)
			{
				normalized[ii]=new Vector2(positions[ii].x*-1, positions[ii].y);
			}
			return normalized;
		}
		return positions;
	}

	public static Vector2 GetRandomAttackingPosition(Vector2 source, Side currentPossession)
	{
		source.x *= currentPossession == Side.PLAYER ? 1 : -1;

		Vector2[] attackingPositions=GetAttackingPositions(source, currentPossession);

		int randIndex=Random.Range(0,attackingPositions.Length);
		return attackingPositions[randIndex];

	}

	public static int GetFormationPointsInPosition(Vector2 pos, Side side)
	{
		if(side==Side.ENEMY)
		{
			if(pos.x==-1)
				return GameManager.instance.stats.enemyTeam.attack;
			else if(pos.x==0)
				return GameManager.instance.stats.enemyTeam.midfield;
			else
				return GameManager.instance.stats.enemyTeam.defence;
		}

		else
		{
			if(pos.x==-1)
				return GameManager.instance.stats.playerTeam.defence;
			else if(pos.x==0)
				return GameManager.instance.stats.playerTeam.midfield;
			else
				return GameManager.instance.stats.playerTeam.attack;
		}
	}

	public static bool IsMoveSuccessful(int value, Vector2 source, Vector2 destination)
	{
		int score=CalculateScoreOnField(value, source, destination);
		Debug.Log("Action score: "+ score);
		if(score>0)
			return true;
		else
			return false;
	}

	public static int CalculateScoreOnField(int value,Vector2 source, Vector2 destination)
	{
		int yourPercent=value*5+RollTheDice()*6;
		int enemyPercent=GetFormationPointsInPosition(destination, Side.ENEMY)*20;
		return yourPercent-enemyPercent;

	}

	public static bool IsComputerShootSuccessful(Side shooterSide)
	{
		Vector2 field;
		Side defenderSide;
		if(shooterSide==Side.PLAYER)
		{
			field=Vector2.right;
			defenderSide=Side.ENEMY;
		}
		else
		{
			field=Vector2.left;
			defenderSide=Side.PLAYER;
		}
		int attackerPoints=GetFormationPointsInPosition(field, shooterSide)+RollTheDice();
		int defenderPoints=GetFormationPointsInPosition(field, defenderSide)+RollTheDice();

		if(attackerPoints>defenderPoints)
			return true;
		else
			return false;
	}

	public static bool CanPlayerStart()
	{
		if(IsPlayerStandingOnBall()&&GameManager.instance.possession==Side.PLAYER)
			return true;
		else
			return false;
	}

	public static bool CanPlayerTackle()
	{
		if(IsPlayerStandingOnBall()&&GameManager.instance.possession==Side.ENEMY)
			return true;
		else
			return false;
	}
		
	public static bool IsPlayerStandingOnBall()
	{
		if(GameManager.instance.player.position==GameManager.instance.ballPosition)
			return true;
		else
			return false;
	}

	public static bool IsPlayerOnPenaltyArea()
	{
		if(GameManager.instance.player.position==Vector2.right)
			return true;
		else
			return false;
	}

	public static int RollTheDice()
	{
		return Random.Range(1, 6);
	}

	public static bool IsBallOnPenaltyArea()
	{
		if(GameManager.instance.ballPosition==Vector2.right||GameManager.instance.ballPosition==Vector2.left)
			return true;
		else
			return false;
	}

	public static bool CanComputerShootOnPenaltyArea()
	{
		if(GameManager.instance.ballPosition==Vector2.right&&GameManager.instance.possession==Side.PLAYER||GameManager.instance.ballPosition==Vector2.left&GameManager.instance.possession==Side.ENEMY)
			return true;
		else
			return false;
	}

	public static Side OtherSide(Side side)
	{
		if(side==Side.PLAYER)
			return Side.ENEMY;
		else
			return Side.PLAYER;
	}
}
