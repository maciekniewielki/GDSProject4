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

	public Attribute(string name, int value, int minValue, int maxValue)
	{
		this.name=name;
		this.value=value;
		this.minValue=minValue;
		this.maxValue=maxValue;
	}
		
	public Attribute(string name, int value)
	{
		this.name=name;
		this.value=value;
		this.minValue=5;
		this.maxValue=20;
	}
}