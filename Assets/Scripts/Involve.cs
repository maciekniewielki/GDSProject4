using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Involve : MonoBehaviour 
{
	public Toggle[] toggles;

	public int currentlySelectedToggle;

	void Start()
	{
		currentlySelectedToggle=1;
	}

	public void SomethingChanged(Toggle t)
	{
		if(!t.isOn)
			return;
		currentlySelectedToggle=int.Parse(t.name);
		if(!GameManager.instance.player.IsEnergyDepleted())
			GameManager.instance.player.SetInvolvement(currentlySelectedToggle);
	}

	int WhichIsOn()
	{
		foreach(Toggle t in toggles)
			if(t.isOn)
				return int.Parse(t.name);
		return 0;
	}

	void InitInvolvement()
	{
		GameManager.instance.player.SetInvolvement(WhichIsOn());
	}
}
