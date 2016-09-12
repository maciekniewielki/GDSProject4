using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatisticsViewer : MonoBehaviour 
{
	public Text playerTeamNameDisplay;
	public Text enemyTeamNameDisplay;
	public Text score;
    public Text ratingText;

    public Text playerTeamShots;
    public Text playerTeamCorners;
    public Text playerTeamFreeKicks;
    public Text playerTeamThrowIns;
    public Text playerTeamFouls;
    public Text playerTeamYellows;
    public Text playerTeamReds;

    public Slider TeamShots;
    public Slider TeamCorners;
    public Slider TeamFreeKicks;
    public Slider TeamThrowIns;
    public Slider TeamFouls;
    public Slider TeamYellows;
    public Slider TeamReds;

    public Text enemyTeamShots;
    public Text enemyTeamCorners;
    public Text enemyTeamFreeKicks;
    public Text enemyTeamThrowIns;
    public Text enemyTeamFouls;
    public Text enemyTeamYellows;
    public Text enemyTeamReds;

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
    public Text playerTurnsOnPitch;

	public GameObject expGainedPrefab;

	private MatchStatistics statisticsToView;
    private Slider[] comparisonBars;

    void Start()
	{
        comparisonBars = new Slider[] { TeamShots, TeamCorners, TeamFreeKicks, TeamThrowIns, TeamFouls, TeamYellows, TeamReds };

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
        foreach (Slider s in comparisonBars)
        {
            s.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = stats.playerTeam.bgColor;
            s.transform.Find("Background").GetComponent<Image>().color = stats.enemyTeam.bgColor;
        }

        playerTeamNameDisplay.text=stats.playerTeam.name;
		enemyTeamNameDisplay.text=stats.enemyTeam.name;
		score.text=stats.playerTeamGoals+":"+stats.enemyTeamGoals;
        TeamNames.text = stats.playerTeam.name + " - " + stats.enemyTeam.name;
        ratingText.text = CalculationsManager.CalculatePlayerRating(stats).ToString();

        //Tabela statów drużyn
        TeamShots.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamShots,stats.enemyTeamShots);
        TeamCorners.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamCorners, stats.enemyTeamCorners);
        TeamFreeKicks.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamFreeKicks, stats.enemyTeamFreeKicks);
        TeamThrowIns.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamThrowIns, stats.enemyTeamThrowIns);
        TeamFouls.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamFouls, stats.enemyTeamFouls);
        TeamYellows.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamYellows, stats.enemyTeamYellows);
        TeamReds.value = CalculationsManager.GetPercentageOfFirstValue(stats.playerTeamReds, stats.enemyTeamReds);

        playerTeamShots.text = stats.playerTeamShots.ToString();
        playerTeamCorners.text = stats.playerTeamCorners.ToString();
        playerTeamFreeKicks.text = stats.playerTeamFreeKicks.ToString();
        playerTeamThrowIns.text = stats.playerTeamThrowIns.ToString();
        playerTeamFouls.text = stats.playerTeamFouls.ToString();
        playerTeamYellows.text = stats.playerTeamYellows.ToString();
        playerTeamReds.text = stats.playerTeamReds.ToString();

        enemyTeamShots.text = stats.enemyTeamShots.ToString();
        enemyTeamCorners.text = stats.enemyTeamCorners.ToString();
        enemyTeamFreeKicks.text = stats.enemyTeamFreeKicks.ToString();
        enemyTeamThrowIns.text= stats.enemyTeamThrowIns.ToString();
        enemyTeamFouls.text = stats.enemyTeamFouls.ToString();
        enemyTeamYellows.text = stats.enemyTeamYellows.ToString();
        enemyTeamReds.text = stats.enemyTeamReds.ToString();

        PlayerName.text=CareerManager.gameInfo.playerStats.playerName+" "+CareerManager.gameInfo.playerStats.playerSurname;
        //Tabela statów piłkarza
        playerTurnsOnPitch.text = "Time Alive: " + stats.playerTurnsOnPitch;
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
			CreateExpPopUp(kvp.Key);

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
