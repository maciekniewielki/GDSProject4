using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CareerStatisticsViewer : MonoBehaviour 
{
    public Text playerShots;

    private CareerStatistics stats;

	void Start () 
	{
        stats = CareerManager.gameInfo.careerStatistics;
        ViewStatistics(stats);
	}

    void ViewStatistics(CareerStatistics s)
    {
        playerShots.text ="Shots: "+ CalculationsManager.StripVector2(s.playerMoves["Shoot"]);
    }
}

