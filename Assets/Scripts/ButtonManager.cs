using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ButtonManager : MonoBehaviour 
{

	public GameObject buttonParent;

	private Dictionary<string, Button> buttons;
	private string[] stageButtons={"passButton", "crossButton"};
	private PitchManager pitch;

	void Awake () 
	{
		pitch=GameObject.Find("Pitch").GetComponent<PitchManager>();
		buttons=new Dictionary<string, Button>();
		foreach(Button g in buttonParent.transform.GetComponentsInChildren<Button>() )
			buttons.Add(g.name, g);
	}

	void Start()
	{
		InitButtons();
	}

	public void InitButtons()
	{
		SetButtonText("shootButton", "Play match");
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
		if(pitch.paused)
		{
			SetInteractableToAll(false);
			SetInteractable("startButton", true);
			return;
		}

		SetInteractable("startButton", false);

		SetInteractableToAll(false);
		if(!pitch.waitingForPlayerInput)
			return;

		SetInteractable("passButton", true);
		Vector2 pos=pitch.board.currentPlayerPosition;
		if(pos!=new Vector2(1,0))
			SetInteractable("crossButton", true);

		if(pos==new Vector2(1,0))
			SetInteractable("shootButton", true);
	}

	public void Click(string which)
	{
		if(which.Equals("startButton"))
		{
			pitch.paused=false;
		}

		SetCurrentlyAvailable();
		if(stageButtons.Contains(which))
			SetInteractable(which, false);

		if(which.Equals("shootButton"))
			pitch.makeMove("FinishShoot");
		else if(which.Equals("passButton"))
		{
			pitch.selectedMove="Pass";
			pitch.Refresh();
			pitch.HighlightSelected(pitch.GetPositions("Pass"));
		}
		else if(which.Equals("crossButton"))
		{
			pitch.selectedMove="Cross";
			pitch.Refresh();
			pitch.HighlightSelected(pitch.GetPositions("Cross"));
		}
		
	}

}
