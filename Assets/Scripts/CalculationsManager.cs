using UnityEngine;
using System.Collections;

public class CalculationsManager: MonoBehaviour
{
	//TODO make it work

	private MatchStatistics stats;
	private Player player;
	private PlayerInfo playerInfo;

	void Start()
	{
		playerInfo=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerInfo;
		stats=GameManager.stats;
	}

	public static Vector2[] GetPositions(string which)
	{
		if(which.Equals("Pass"))
			return GetPassingPositions();
		else
			return GetCrossingPositions();
	}

	public static Vector2[] GetCrossingPositions()
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

	public static Vector2[] GetAttackingPositions(Vector2 source)
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

	public static Vector2 GetRandomAttackingPosition(Vector2 source, Side currentPossession)
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

	public static int GetFormationPointsInPosition(Vector2 pos, Side side)
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

	public static bool IsMoveSuccessful(int value, Vector2 source, Vector2 destination)
	{
		int score=CalculateScoreOnField(value, source, destination);
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

	public static bool IsComputerShootSuccessful(Side shooterSide, Side defenderSide)
	{
		Vector2 field;
		if(shooterSide==Side.PLAYER)
			field=Vector2.right;
		else
			field=Vector2.left;
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
		if(player.position==GameManager.instance.ballPosition)
			return true;
		else
			return false;
	}

	public static bool IsPlayerOnPenaltyArea()
	{
		if(player.position==Vector2.right)
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
}
