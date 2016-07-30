using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class Field : MonoBehaviour 
{
	//TODO make it work

	public Sprite passHighlight;
	public Sprite notHighlighted;

	private SpriteRenderer sRenderer;
	private bool highlighted;

	void Start()
	{
		sRenderer=GetComponent<SpriteRenderer>();
		sRenderer.sprite=notHighlighted;
		UnHighlight();
	}
	public void Highlight()
	{
		sRenderer.sprite=passHighlight;
		highlighted=true;
		sRenderer.color=new Color(1,1,1);
	}
	public void UnHighlight()
	{
		highlighted=false;
		sRenderer.sprite=notHighlighted;
		sRenderer.color=new Color(0.8f,0.8f,0.8f);
	}
		

	void OnMouseDown()
	{
		if(!GameManager.instance.gameStarted)
		{
			GameManager.instance.player.MoveYourself(NameToVector(name));
			GameManager.instance.player.preferredPosition=NameToVector(name);
		}

		Debug.Log("Clicked field: "+ NameToVector(name));

		if(GameManager.instance.IsGameHardPaused())
			return;

		if(GameManager.instance.GetSelectedMove()!=null&&highlighted)
			GameManager.instance.MakeSelectedMove(NameToVector(name));
	}

	Vector2 NameToVector(string name)
	{
		int number=int.Parse(name);
		int y;

		if(number<4)
			y=1;
		else if(number<7)
			y=0;
		else
			y=-1;
		return new Vector2((number-1)%3-1, y);
	}
}
