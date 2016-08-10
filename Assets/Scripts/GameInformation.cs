using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameInformation 
{
	public bool wentToIndividualTraining;
	public bool wentToClubTraining;
	public int currentRound;
	public int currentWeekDay;
	public PlayerInfo playerStats;

	override
	public string ToString()
	{
		string s="";
		s+="Current round: "+ currentRound+"\n";
		s+="Current day: "+ currentWeekDay+"\n";
		s+="Went to Individual Training: "+ wentToIndividualTraining+"\n";
		s+="Went to club Training"+ wentToClubTraining+"\n";

		return s;
	}
}
