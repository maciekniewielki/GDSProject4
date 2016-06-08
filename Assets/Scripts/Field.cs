using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class Field : MonoBehaviour 
{
	public Sprite passHighlight;
	public Sprite notHighlighted;
	public GameObject playerHighlight;

	private SpriteRenderer sRenderer;
	private PitchManager manager;
	private bool highlighted;

	void Awake()
	{
		sRenderer=GetComponent<SpriteRenderer>();
		sRenderer.sprite=notHighlighted;
		manager=GameObject.Find("Pitch").GetComponent<PitchManager>();
		highlighted=false;
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
		

	public void Highlight(string what)
	{
		if(what.Equals("Player"))
		{
			playerHighlight.transform.position=transform.position;
		}
	}

	void OnMouseDown()
	{
		if(!manager.HasTheGameStarted())
			manager.SetPlayerPosition(NameToVector(name));

		if(highlighted)
		{
			manager.SetMoveDestination(NameToVector(name));
			manager.makeMove();
		}
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
