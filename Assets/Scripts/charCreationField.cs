using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class charCreationField : MonoBehaviour, IPointerClickHandler
{
	public Sprite passHighlight;
	public Sprite notHighlighted;
	public EventHandler eventHandler;

	private Image sRenderer;
	private bool highlighted;

	void Awake()
	{
		sRenderer=GetComponent<Image>();
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
		Debug.Log("Clicked!");
		eventHandler.ClickedField(NameToVector(name));
		
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

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		OnMouseDown();
	}

	#endregion
}
