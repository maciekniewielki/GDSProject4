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

	/// <summary>
	/// Initializes a new instance of the <see cref="SerializableColor"/> class.
	/// </summary>
	/// <param name="r">The red component. Range 0-255</param>
	/// <param name="g">The green component. Range 0-255</param>
	/// <param name="b">The blue component. Range 0-255</param>
	/// <param name="a">The alpha component. Range 0-255</param>
	public SerializableColor(float r, float g, float b, float a)
	{
		this.r = r/255;
		this.g = g/255;
		this.b = b/255;
		this.a = a/255;
	}

	public SerializableColor(float r, float g, float b)
	{
		this.r = r/255;
		this.g = g/255;
		this.b = b/255;
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

