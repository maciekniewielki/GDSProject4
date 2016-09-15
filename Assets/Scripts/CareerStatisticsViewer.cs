using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CareerStatisticsViewer : MonoBehaviour 
{
    public Text PlayerGoals;
    public Text playerShots;
    public Text playerPassess;
    public Text playerDribbles;
    public Text playerTackles;
    public Text playerCrosses;
    public Text playerCorners;
    public Text playerLongShots;
    public Text playerThrowIns;
    public Text playerFreeKicks;
    public Text playerPenalties;
    public Text playerHeaders;
    public Text playerFouls;
    public Text playerYellows;
    public Text playerReds;
    public Text playerTurnsOnPitch;

    private CareerStatistics stats;

	void Start () 
	{
        stats = CareerManager.gameInfo.careerStatistics;
        ViewStatistics(stats);
	}

    void ViewStatistics(CareerStatistics s)
    {
        playerTurnsOnPitch.text = "Minutes played: " + s.playerTurnsOnPitch;
        PlayerGoals.text = "Goals: " + s.playerGoals.ToString();
        playerShots.text = "Shots: " + CalculationsManager.StripVector2(s.playerMoves["Shoot"]);
        playerPassess.text = "Passess: " + CalculationsManager.StripVector2(s.playerMoves["Pass"]);
        playerDribbles.text = "Dribbles: " + CalculationsManager.StripVector2(s.playerMoves["Dribble"]);
        playerTackles.text = "Tackles: " + CalculationsManager.StripVector2(s.playerMoves["Tackle"]);
        playerCrosses.text = "Crosses: " + CalculationsManager.StripVector2(s.playerMoves["Cross"]);
        playerCorners.text = "Corners: " + CalculationsManager.StripVector2(s.playerMoves["Corner"]);
        playerLongShots.text = "Long Shots: " + CalculationsManager.StripVector2(s.playerMoves["LongShot"]);
        playerThrowIns.text = "Throw Ins: " + CalculationsManager.StripVector2(s.playerMoves["Out"]);
        playerFreeKicks.text = "Free Kicks: " + CalculationsManager.StripVector2(s.playerMoves["FreeKick"]);
        playerPenalties.text = "Penalties: " + CalculationsManager.StripVector2(s.playerMoves["Penalty"]);
        playerHeaders.text = "Headers: " + CalculationsManager.StripVector2(s.playerMoves["Head"]);
        playerFouls.text = "Fouls: " + s.playerFouls.ToString();
        playerYellows.text = "Yellow cards: " + s.playerYellows.ToString();
        playerReds.text = "Red cards: " + s.playerReds.ToString();
    }
}

