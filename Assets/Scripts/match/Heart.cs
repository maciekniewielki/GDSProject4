using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Heart : MonoBehaviour, IPointerClickHandler
{

	public Sprite highlightSprite;
	public Sprite unhighlightSprite;

	private Involve parent;
	private int myNumber;

	void Start () 
	{
		myNumber=int.Parse(name);
		parent=GetComponentInParent<Involve>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		parent.Click(myNumber);
	}

	public void Highlight()
	{
		gameObject.GetComponent<Image>().sprite=highlightSprite;
	}

	public void Unhighlight()
	{
		gameObject.GetComponent<Image>().sprite=unhighlightSprite;
	}
}
