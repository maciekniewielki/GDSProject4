using UnityEngine;
[System.Serializable]
public class SerializableColor
{
	public float r, g, b, a;

	public SerializableColor(Color c)
	{
		r = c.r;
		g = c.g;
		b = c.b;
		a = c.a;
	}

	public SerializableColor(float r, float g, float b, float a)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}

	public SerializableColor(float r, float g, float b)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = 1f;
	}

	public static implicit operator Color(SerializableColor rValue)
	{
		return new Color(rValue.r, rValue.g, rValue.b, rValue.a);
	}

	public static implicit operator SerializableColor(Color rValue)
	{
		return new SerializableColor(rValue);
	}
}

