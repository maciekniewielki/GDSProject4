using UnityEngine;
using System.Collections;

public class Contusion
{
	public ContusionType type;
	public int daysToHeal;
	public string message;
	public int minDaysToHeal;
	public int maxDaysToHeal;
	public float probability;

	public Contusion(ContusionType type, int minDaysToHeal, int maxDaysToHeal, float probability)
	{
		this.type=type;
		daysToHeal=Random.Range(minDaysToHeal, maxDaysToHeal+1);
		this.probability=probability;
	}

};

public enum ContusionType{ANKLE_SPRAIN, ACHILLES_TEAR, ACHILLES_RUPTURE, GASTROSOLEUS_TEAR, KNEE_INJURY, TIBIAL_LIGAMENT_INJURY, THIGHT_CONTUSION, MUSCLE_FIBER_RUPTURE, CRUCIATE_LIGAMENT_RUPTURE};