using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//TODO make it work
	public Vector2 position;
	public PlayerInfo playerInfo;
	/*
	void Pass(Vector2 destination)
	{
		


		Refresh();
		waitingForPlayerInput=false;
	}

	bool Tackle()
	{
		int yourPercent=playerTackling*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(board.currentPlayerPosition)*20;

		if(yourPercent>=enemyPercent)
		{
			AddText("Tackle successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.tacklesSuccessful++;
			ChangePossession();
			return true;
		}
		else
		{
			AddText("Tackle unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.tacklesUnsuccessfull++;
			return false;
		}
	}

	void Cross()
	{
		bool found=false;
		if(new Vector2(1,0).Equals(moveDestination))
			found=true;

		if(!found)
			return;

		int yourPercent=playerCrossing*5+RollTheDice()*6;
		int enemyPercent=GetEnemyFormationPointsInPosition(moveDestination)*20;
		SetCurrentBallPosition(moveDestination);
		if(yourPercent>=enemyPercent)
		{
			AddText("Cross successful("+ yourPercent + "to " + enemyPercent + ")");
			stats.crossesSuccessful++;
		}
		else
		{
			AddText("Cross unsuccessful("+ yourPercent + "to " + enemyPercent + ")");
			stats.crossesUnsuccessfull++;
			ChangePossession();
		}
		Refresh();
		waitingForPlayerInput=false;
	}*/
		
}
