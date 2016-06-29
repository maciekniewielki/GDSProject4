using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ButtonManager : MonoBehaviour 
{
	//TODO add finishshoot implementation

	public GameObject buttonParent;

	private Dictionary<string, Button> buttons;
	private string[] stageButtons={"passButton", "crossButton", "dribbleButton"};
	private PitchManager pitch;

	void Start () 
	{
		GameManager.instance.onPlayerTurnStart+=SetCurrentlyAvailable;
		GameManager.instance.onPlayerTurnEnd+=SetCurrentlyAvailable;
		GameManager.instance.onMatchEnd+=SetCurrentlyAvailable;
		pitch=GameObject.Find("Pitch").GetComponent<PitchManager>();
		buttons=new Dictionary<string, Button>();
		foreach(Button g in buttonParent.transform.GetComponentsInChildren<Button>() )
			buttons.Add(g.name, g);

		InitButtons();
	}
		

	public void InitButtons()
	{
		SetButtonText("startButton", "Play match");
	}
		


	public void SetInteractableToAll(bool val)
	{
		foreach(KeyValuePair<string, Button> pair in buttons)
			pair.Value.interactable=val;
	}

	public void SetInteractable(string which, bool val)
	{
		buttons[which].interactable=val;
	}

	public void SetButtonText(string which, string txt)
	{
		buttons[which].GetComponentInChildren<Text>().text=txt;
	}

	public void SetCurrentlyAvailable()
	{
		SetInteractableToAll(false);

		if(!GameManager.instance.gameStarted)
			SetInteractable("startButton", true);

		if(!GameManager.instance.playerTurn)
			return;

		if(GameManager.instance.playerHasBall)
		{
			SetInteractable("dribbleButton", true);
			SetInteractable("passButton", true);
			if(CalculationsManager.IsPlayerOnPenaltyArea())
				SetInteractable("shootButton", true);
			else
			{
				SetInteractable("longShotButton", true);
				SetInteractable("crossButton", true);
			}
		}
	}

	public void Click(string which)
	{

		if(!stageButtons.Contains(which))
		{
			if(which.Equals("startButton"))
			{
				GameManager.instance.StartTheMatch();
				SetInteractable("startButton", false);
			}
			else if(which.Equals("shootButton"))
				GameManager.instance.MakeMove("Shoot", Vector2.right);
			else if(which.Equals("longShotButton"))
				GameManager.instance.MakeMove("LongShot", Vector2.right);
		}
		else
		{
			SetCurrentlyAvailable();
			SetInteractable(which, false);
			string move=(which.First().ToString().ToUpper()+which.Substring(1)).Remove(which.Length-6);
			Debug.Log("Selected move: "+move);


			GameManager.instance.SetSelectedMove(move);
			pitch.HighlightSelected(CalculationsManager.GetPositions(move, GameManager.instance.GetPlayerPosition()));

		}
		
	}

}
