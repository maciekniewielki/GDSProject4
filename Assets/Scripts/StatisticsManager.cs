using UnityEngine;
using System.Collections;

public class StatisticsManager : MonoBehaviour 
{

	public MatchStatistics endStatistics;
	void Awake () 
	{
		GameManager.instance.onMatchEnd+=SaveStatistics;
		DontDestroyOnLoad(gameObject);
	}

	void SaveStatistics()
	{
		endStatistics=GameManager.instance.stats;
	}
}
