using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesignToolManager : MonoBehaviour {

	//TODO add designer screen
	//Debug
	private int playerPassing;
	private int playerTackling;
	private int gameSpeed;
	private int playerFinishing;
	private int playerCrossing;

	public Text[] logs;
	public Text possession;
	public GameObject designerSliders;

	// Use this for initialization
	void Start () 
	{
		GameManager g=GameManager.instance;
		Player p=g.player;
		g.onCornerBegin+=Corner;
		//g.onPlayerTeamGoal+=PlayerTeamGoal;
		//g.onPlayerTeamMiss+=PlayerTeamMiss;
		//g.onEnemyTeamGoal+=EnemyTeamGoal;
		//g.onEnemyTeamMiss+=EnemyTeamMiss;
		g.onChangePossession+=UpdatePossession;
		g.onMatchStart+=InitLogs;
		g.onMatchEnd+=OnMatchEnd;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown("d"))
			designerSliders.SetActive(!designerSliders.activeSelf);
	}

	void InitLogs()
	{
		/*
		foreach(Text t in logs)
			if(t.Equals(logs[0]))
				t.text="Logs: \n";
			else
				t.text="";*/
		logs[0].text="";
	}

	void OnMatchEnd()
	{
		AddText("Match Ended");
		AddText("Tutaj kiedys beda statystyki");
	}

	void ActionSuccess()
	{
		AddText("Action success!");
	}

	void ActionFail()
	{
		AddText("Action Failed!");
	}

	void Corner()
	{
		if(GameManager.instance.possession==Side.PLAYER)
			AddText("Corner for the " + GameManager.instance.stats.playerTeam.name);
		else
			AddText("Corner for the " + GameManager.instance.stats.enemyTeam.name);
	}

	void UpdatePossession()
	{
		if(GameManager.instance.possession==Side.PLAYER)
			possession.text="Current possession: Player Team";
		else
			possession.text="Current possession: Enemy Team";
	}

	void PlayerTeamGoal()
	{
		AddText("Player Team has Scored");
	}

	void EnemyTeamGoal()
	{
		AddText("Enemy Team has Scored");
	}

	void PlayerTeamMiss()
	{
		AddText("Player Team has Missed");
	}

	void EnemyTeamMiss()
	{
		AddText("Enemy Team has Missed");
	}

	public void AddText(string what)
	{
		/*
		int numberOfTurns = GameManager.instance.currentMinute;
		if(numberOfTurns>90)
		{
			logs[3].text = logs[3].text + what + "\n";
			return;
		}

		if (numberOfTurns < 23)
			logs[0].text = logs[0].text + numberOfTurns+"':"+ what + "\n";
		else if(numberOfTurns < 46)
			logs[1].text = logs[1].text + numberOfTurns+"':" + what + "\n";
		else if(numberOfTurns<69)
			logs[2].text = logs[2].text + numberOfTurns+"':" + what + "\n";
		else
			logs[3].text = logs [3].text + numberOfTurns+"':" + what + "\n";
			*/

		logs[0].text = what;
	}


	public void SetPlayerAttack(float attack)
	{
		GameManager.instance.stats.playerTeam.attack = (int)attack;
	}
	public void SetPlayerMidfield(float midfield)
	{
		GameManager.instance.stats.playerTeam.midfield = (int)midfield;
	}
	public void SetPlayerDefence(float defence)
	{
		GameManager.instance.stats.playerTeam.defence = (int)defence;
	}
	public void SetEnemyAttack(float attack)
	{
		GameManager.instance.stats.enemyTeam.attack = (int)attack;
	}
	public void SetEnemyMidfield(float midfield)
	{
		GameManager.instance.stats.enemyTeam.midfield = (int)midfield;
	}
	public void SetEnemyDefence(float defence)
	{
		GameManager.instance.stats.enemyTeam.defence = (int)defence;
	}

	public void SetPlayerPassing(float passing)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Passing"].value=(int)passing;
		Debug.Log("current Passing: "+ GameManager.instance.player.playerInfo.playerAttributes["Passing"].value);
	}
	public void SetPlayerTackling(float tackling)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Tackling"].value=(int)tackling;
		Debug.Log("current Tackling "+ GameManager.instance.player.playerInfo.playerAttributes["Tackling"].value);
	}
	public void SetPlayerCrossing(float crossing)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Crossing"].value=(int)crossing;
		Debug.Log("current Crossing "+ GameManager.instance.player.playerInfo.playerAttributes["Crossing"].value);
	}
	public void SetGameSpeed(float speed)
	{
		GameManager.instance.gameSpeed=(int)speed;
	}
	public void SetPlayerFinishing(float finish)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Finishing"].value=(int)finish;
		Debug.Log("current Finishing "+ GameManager.instance.player.playerInfo.playerAttributes["Finishing"].value);
	}
	public void SetPlayerDribbling(float dribbling)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Dribbling"].value=(int)dribbling;
		Debug.Log("current Dribbling "+ GameManager.instance.player.playerInfo.playerAttributes["Dribbling"].value);
	}
	public void SetPlayerLongShots(float longShots)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Long Shots"].value=(int)longShots;
		Debug.Log("current Long Shots "+ GameManager.instance.player.playerInfo.playerAttributes["Long Shots"].value);
	}

	public void SetPlayerStamina(float stamina)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Stamina"].value=(int)stamina;
		Debug.Log("current Stamina "+ GameManager.instance.player.playerInfo.playerAttributes["Stamina"].value);
	}

	public void SetPlayerCorners(float corners)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Corners"].value=(int)corners;
		Debug.Log("current Corners "+ GameManager.instance.player.playerInfo.playerAttributes["Corners"].value);
	}
		
	public void SetPlayerLongThrows(float longThrows)
	{
		GameManager.instance.player.playerInfo.playerAttributes["Long Throws"].value=(int)longThrows;
		Debug.Log("current Long Throws "+ GameManager.instance.player.playerInfo.playerAttributes["Long Throws"].value);
	}
}
