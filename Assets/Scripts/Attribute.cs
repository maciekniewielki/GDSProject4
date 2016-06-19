using UnityEngine;
using System.Collections;

[System.Serializable]
public class Attribute
{
	public string name;
	public int value;
	public int maxValue;
	public int minValue;

	override
	public string ToString()
	{
		return "name: " + name.ToString() + "\n" + "value: " + value.ToString() + "\n";
	}
}