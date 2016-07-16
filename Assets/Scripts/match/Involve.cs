using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Involve : MonoBehaviour 
{
	public Heart[] hearts;

	public int currentlySelectedHeart;

	void Start()
	{
		currentlySelectedHeart=1;
		SetHeartsHighlight(1);
	}

	void InitInvolvement()
	{
		GameManager.instance.player.SetInvolvement(currentlySelectedHeart);
	}

	public void Click(int which)
	{
		if(which!=currentlySelectedHeart)
		{
			currentlySelectedHeart=which;
			SetHeartsHighlight(currentlySelectedHeart);
			if(!GameManager.instance.player.IsEnergyDepleted())
				GameManager.instance.player.SetInvolvement(currentlySelectedHeart);
		}
	}

	void SetHeartsHighlight(int involveLevel)
	{
		for(int ii=0;ii<hearts.Length;ii++)
		{
			if(ii+1<=involveLevel)
				hearts[ii].Highlight();
			else
				hearts[ii].Unhighlight();
		}
	}
}
