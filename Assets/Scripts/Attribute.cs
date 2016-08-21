using UnityEngine;
using System.Collections;

[System.Serializable]
public class Attribute
{
	public string name;
	public int value;
	public int maxValue;
	public int minValue;
	public int currentExp;

	override
	public string ToString()
	{
		return "name: " + name.ToString() + "\n" + "value: " + value.ToString() + "\n";
	}

	public Attribute(string name, int value, int minValue, int maxValue)
	{
		this.name=name;
		SetStartingAttributeValueAndExp(value);
		this.minValue=minValue;
		this.maxValue=maxValue;
	}
		
	public Attribute(string name, int value)
	{
		this.name=name;
		SetStartingAttributeValueAndExp(value);
		this.minValue=5;
		this.maxValue=20;
	}

	public void SetStartingAttributeValueAndExp(int value)
	{
		this.value=value;
		this.currentExp=CalculationsManager.GetStartingExpByLevel(value);
	}

	public void IncrementStartingAttributeValue()
	{
		SetStartingAttributeValueAndExp(this.value+1);
	}

	public void DecrementStartingAttributeValue()
	{
		SetStartingAttributeValueAndExp(this.value-1);
	}

	public void AddExp(int exp)
	{
		this.currentExp+=exp;
		this.value=CalculationsManager.GetLevelByExp(this.currentExp);
	}

	public float GetCurrentExpPercent()
	{
		float currentLevelExp=CalculationsManager.GetStartingExpByLevel(value);
		float nextLevelExp=CalculationsManager.GetStartingExpByLevel(value+1);
		float percent=(currentExp-currentLevelExp)*100f/(nextLevelExp-currentLevelExp);

		return percent;
	}
}