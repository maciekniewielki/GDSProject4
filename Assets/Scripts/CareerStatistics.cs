using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CareerStatistics : MonoBehaviour
{
    public Text playerShots;
    public int ShotsSum;
    private MatchStatistics statisticsToView;
    
    void Start ()
    {
        MatchStatistics stats = GameObject.Find("MatchStats").GetComponent<StatisticsManager>().endStatistics;
        statisticsToView = stats;
        ViewStatistics(stats);
    }

    public void ViewStatistics(MatchStatistics stats)
    {
        playerShots.text = "Shots: " + ShotsSum.ToString();
        ShotsSum += stats.playerTeamShots;
    }
}
