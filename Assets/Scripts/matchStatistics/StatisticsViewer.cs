using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatisticsViewer : MonoBehaviour 
{
	public Text playerTeamNameDisplay;
	public Text enemyTeamNameDisplay;
	public Text score;
    public Text TeamShots;
    public Text TeamCorners;
    public Text TeamFreeKicks;
    public Text TeamThrowIns;
    public Text TeamFouls;
    public Text TeamYellows;
    public Text TeamReds;
    public Text TeamNames;
    public Text ShotsSign;
    public Text PlayerName;
    public Text PlayerGoals;
    public Text playerShots;
    public Text playerLongShots;
    public Text playerPassess;
    public Text playerDribbles;
    public Text playerTackles;
    public Text playerCorners;
    public Text playerCrossing;
    public Text playerFreeKicks;
    public Text playerThrowIns;
    public Text playerHeaders;
    public Text playerFouls;
    public Text playerYellows;
    public Text playerReds;

	public GameObject expGainedPrefab;

	private MatchStatistics statisticsToView;
    

    void Start()
	{
		MatchStatistics stats= GameObject.Find("MatchStats").GetComponent<StatisticsManager>().endStatistics;

		/*
		Team playerTeam=new Team("Real Madrid", 1,1,1);
		Team enemyTeam=new Team("Chelsea", 1,1,1);
		MatchStatistics stats=new MatchStatistics(playerTeam, enemyTeam);
		stats.playerTeamGoals=2;
		stats.enemyTeamGoals=1;
		stats.playerMoves["Dribble"]=new Vector2(3,5);
		CareerManager.gameInfo=new GameInformation(1, 0, new PlayerInfo());*/

		statisticsToView=stats;
		ViewStatistics(stats);
		//Debug.Log(stats.playerMoves["Dribble"]);
		//CalculationsManager.stripVector2(stats.playerMoves["Dribble"]);
		//stats.playerMoves["Dribble"].x+"/"+stats.playerMoves["Dribble"].y;
	}

	public void ViewStatistics(MatchStatistics stats)
	{
		playerTeamNameDisplay.text=stats.playerTeam.name;
		enemyTeamNameDisplay.text=stats.enemyTeam.name;
		score.text=stats.playerTeamGoals+":"+stats.enemyTeamGoals;
        TeamNames.text = stats.playerTeam.name + " - " + stats.enemyTeam.name;

        //Tabela statów drużyn
        TeamShots.text = stats.playerTeamShots.ToString()+" - "+stats.enemyTeamShots.ToString();
        TeamCorners.text = stats.playerTeamCorners.ToString() + " - " + stats.enemyTeamCorners.ToString();
        TeamFreeKicks.text = stats.playerTeamFreeKicks.ToString() + " - " + stats.enemyTeamFreeKicks.ToString();
        TeamThrowIns.text = stats.playerTeamThrowIns.ToString() + " - " + stats.enemyTeamThrowIns.ToString();
        TeamFouls.text = stats.playerTeamFouls.ToString() + " - " + stats.enemyTeamFouls.ToString();
        TeamYellows.text = stats.playerTeamYellows.ToString() + " - " + stats.enemyTeamYellows.ToString();
        TeamReds.text = stats.playerTeamReds.ToString() + " - " + stats.enemyTeamReds.ToString();
        
		PlayerName.text=CareerManager.gameInfo.playerStats.playerName+" "+CareerManager.gameInfo.playerStats.playerSurname;
        //Tabela statów piłkarza
        PlayerGoals.text = "Goals: " + stats.playerGoals.ToString();
        playerShots.text = "Shots: " + CalculationsManager.StripVector2(stats.playerMoves["Shoot"]);
        playerLongShots.text = "Long Shots: " + CalculationsManager.StripVector2(stats.playerMoves["LongShot"]);
        playerPassess.text = "Passess: "+CalculationsManager.StripVector2(stats.playerMoves["Pass"]);
        playerDribbles.text = "Dribbles: " + CalculationsManager.StripVector2(stats.playerMoves["Dribble"]);
        playerTackles.text = "Tackles: "+ CalculationsManager.StripVector2(stats.playerMoves["Tackle"]);
        playerCorners.text = "Corners: "+ CalculationsManager.StripVector2(stats.playerMoves["Corner"]);
        playerFreeKicks.text = "Free Kicks: "+ CalculationsManager.StripVector2(stats.playerMoves["FreeKick"]);
        playerThrowIns.text = "Throw-ins: "+ CalculationsManager.StripVector2(stats.playerMoves["Out"]);
        playerHeaders.text = "Headers: " + CalculationsManager.StripVector2(stats.playerMoves["Head"]);
        playerCrossing.text = "Crosses: "+ CalculationsManager.StripVector2(stats.playerMoves["Cross"]);
        playerFouls.text = "Fouls: " + stats.playerFouls.ToString();
        playerYellows.text = "Yellow cards: " + stats.playerYellows.ToString();
        playerReds.text = "Red cards: " + stats.playerReds.ToString();

		foreach(KeyValuePair<string, Vector2> kvp in stats.playerMoves)
		{
			Debug.Log("Penis");
			CreateExpPopUp(kvp.Key);
		}

    }

	void CreateExpPopUp(string move)
	{
		Text statText;

		if(move.Equals("Shoot"))
			statText=playerShots;
		else if(move.Equals("LongShot"))
			statText=playerLongShots;
		else if(move.Equals("Pass"))
			statText=playerPassess;
		else if(move.Equals("Dribble"))
			statText=playerDribbles;
		else if(move.Equals("Tackle"))
			statText=playerTackles;
		else if(move.Equals("Corner"))
			statText=playerCorners;
		else if(move.Equals("FreeKick"))
			statText=playerFreeKicks;
		else if(move.Equals("Out"))
			statText=playerThrowIns;
		else if(move.Equals("Head"))
			statText=playerHeaders;
		else if(move.Equals("Cross"))
			statText=playerCrossing;
		else 
			return;

		int expGained=CalculationsManager.GetExpBySkillUsage(statisticsToView.playerMoves[move]);
		if(expGained<=0)
			return;

		Text text=Instantiate(expGainedPrefab).GetComponent<Text>();
		text.transform.SetParent(GameObject.Find("Canvas").transform);
		text.transform.position=statText.transform.position+Vector3.right*230;
		text.text="+"+expGained+" Exp";

		CareerManager.gameInfo.playerStats.playerAttributes[CalculationsManager.MoveNameToSkillName(move)].AddExp(expGained);
	}

}
