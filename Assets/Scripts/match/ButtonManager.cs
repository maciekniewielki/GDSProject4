using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
	//TODO add finishshoot implementation

	public GameObject buttonParent;

	private Dictionary<string, Button> buttons;
	private string[] stageButtons = { "passButton", "crossButton", "dribbleButton", "moveButton", "outButton" };
	private PitchManager pitch;

	private bool shortOut;

	void Start()
	{
		GameManager.instance.onFreeKickBegin += SetFreeKickButtonToPressed;
		GameManager.instance.onTurnStart += SetCurrentlyAvailable;
		GameManager.instance.onCornerEnd += SetCurrentlyAvailable;
		GameManager.instance.onCornerBegin += OnRestartMoveBegin;
		GameManager.instance.onOutBegin += OnRestartMoveBegin;
		GameManager.instance.onPlayerTurnEnd += SetCurrentlyAvailable;
		GameManager.instance.onPlayerTurnStart += SetCurrentlyAvailable;
		GameManager.instance.onMatchEnd += SetCurrentlyAvailable;
		GameManager.instance.onMatchEnd += TurnOffTheStartButton;
		GameManager.instance.onPause += OnGamePause;
		GameManager.instance.onUnpause += OnGamePause;
		GameManager.instance.onHardPause += OnGamePause;
		GameManager.instance.onHardUnpause += OnGamePause;
		GameManager.instance.onHalfTime += OnHalfTime;
		pitch = GameObject.Find("Pitch").GetComponent<PitchManager>();
		buttons = new Dictionary<string, Button>();
		foreach(Button g in buttonParent.transform.GetComponentsInChildren<Button>())
			buttons.Add(g.name, g);

		InitButtons();
	}

	public void InitButtons()
	{
		SetButtonText("startButton", "Play match");
	}

	void OnGamePause()
	{
		SetCurrentlyAvailable();
	}

	void OnHalfTime()
	{
		OnGamePause();
		SetButtonText("startButton", "2nd Half");
	}

	void OnRestartMoveBegin()
	{
		if(GameManager.instance.nextAction.isPlayerPerforming && GameManager.instance.nextAction.type == RestartActionType.CORNER)
			SetInteractable("cornerButton", true);
		else if(GameManager.instance.nextAction.isPlayerPerforming && GameManager.instance.nextAction.type == RestartActionType.OUT)
			SetInteractable("outButton", true);
	}

	public void SetInteractableToAll(bool val)
	{
		foreach(KeyValuePair<string, Button> pair in buttons)
			pair.Value.interactable = val;
	}

	public void SetInteractable(string which, bool val)
	{
		buttons[which].interactable = val;
	}

	public void SetColorToPressed(string which)
	{
		ColorBlock colors = buttons[which].colors;
		colors.pressedColor = colors.normalColor;
		buttons[which].colors = colors;
	}

	public void SetFreeKickButtonToPressed()
	{
		SetColorToPressed("freekickButton");
	}

	public void SetButtonText(string which, string txt)
	{
		buttons[which].GetComponentInChildren<Text>().text = txt;
	}

	public void SetCurrentlyAvailable()
	{
		SetInteractableToAll(false);
		if(GameManager.instance.IsGameHardPaused())
			return;
		SetInteractable("startButton", true);


		if(GameManager.instance.currentMinute != 46)
		{
			if(GameManager.instance.gameStarted)
			{
				if(!GameManager.instance.IsGamePaused())
					SetButtonText("startButton", "Pause");
				else
					SetButtonText("startButton", "Play");

			}
			else
				SetButtonText("startButton", "Play Match");
		}
			
		if(GameManager.instance.IsGamePaused() || GameManager.instance.player.IsEnergyDepleted() || GameManager.instance.player.contusion != null || GameManager.instance.player.HasRed())
			return;

		if(!GameManager.instance.playerHasBall && !GameManager.instance.playerRestartMoveRemaining && !GameManager.instance.IsGamePaused() && !GameManager.instance.player.movedThisTurn)
			SetInteractable("moveButton", true);

		if(GameManager.instance.IsPlayerWaitingForRestart())
		{
			if(GameManager.instance.nextAction.type == RestartActionType.CORNER)
				SetInteractable("cornerButton", true);
			else if(GameManager.instance.nextAction.type == RestartActionType.OUT)
				SetInteractable("outButton", true);
			else if(GameManager.instance.nextAction.type == RestartActionType.HEAD)
				SetInteractable("headButton", true);
			else if(GameManager.instance.nextAction.type == RestartActionType.FREEKICK)
				SetInteractable("freekickButton", true);
			else if(GameManager.instance.nextAction.type == RestartActionType.PENALTY)
				SetInteractable("penaltyButton", true);
			return;
		}

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
		if((which.First().ToString().ToUpper() + which.Substring(1)).Remove(which.Length - 6).Equals("Head"))
		{
			if(GameManager.instance.nextAction.source == Vector2.right)
				GameManager.instance.MakeMove("Head", Vector2.right);
			else
			{
				SetCurrentlyAvailable();
				GameManager.instance.SetSelectedMove("Head");
				pitch.HighlightSelected(CalculationsManager.GetPositions("Head", GameManager.instance.GetPlayerPosition()));
			}
		}

		if(!stageButtons.Contains(which))
		{
			pitch.UnHighlightEverything();
			if(which.Equals("startButton"))
			{
				if(!GameManager.instance.HasTheGameStarted())
					GameManager.instance.StartTheMatch();
				else if(GameManager.instance.IsGamePaused())
					GameManager.instance.Unpause();
				else
					GameManager.instance.Pause();

			}
			else if(which.Equals("shootButton"))
				GameManager.instance.MakeMove("Shoot", Vector2.right);
			else if(which.Equals("longShotButton"))
				GameManager.instance.MakeMove("LongShot", Vector2.right);
		}
		else
		{
			SetCurrentlyAvailable();
			string move;
			if(!which.Equals("outButton"))
			{
				pitch.UnHighlightEverything();
				SetInteractable(which, false);
				move = (which.First().ToString().ToUpper() + which.Substring(1)).Remove(which.Length - 6);
			}
			else
			{
				shortOut = !shortOut;
				if(shortOut)
					move = "Out";
				else
					move = "LongOut";
			}
			Debug.Log("Selected move: " + move);

			if(move.Equals("Move"))
				GameManager.instance.Pause();

			GameManager.instance.SetSelectedMove(move);
			pitch.HighlightSelected(CalculationsManager.GetPositions(move, GameManager.instance.GetPlayerPosition()));

		}
		
	}

	void TurnOffTheStartButton()
	{
		SetInteractable("startButton", false);
	}
}
