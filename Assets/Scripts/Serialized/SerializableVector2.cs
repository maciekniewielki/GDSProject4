using UnityEngine;

[System.Serializable]
public struct SerializableVector2
{

	public float x;
	public float y;


	public SerializableVector2(float rX, float rY)
	{
		x = rX;
		y = rY;
	}
		
	public static implicit operator Vector2(SerializableVector2 rValue)
	{
		return new Vector2(rValue.x, rValue.y);
	}
		
	public static implicit operator SerializableVector2(Vector2 rValue)
	{
		return new SerializableVector2(rValue.x, rValue.y);
	}

    public static SerializableVector2 operator+(SerializableVector2 leftValue ,SerializableVector2 rightValue)
    {
        return new SerializableVector2(leftValue.x + rightValue.x, leftValue.y + rightValue.y);
    }

    public static SerializableVector2 operator+(SerializableVector2 leftValue, Vector2 rightValue)
    {
        return new SerializableVector2(leftValue.x + rightValue.x, leftValue.y + rightValue.y);
    }

    public static SerializableVector2 operator+(Vector2 leftValue, SerializableVector2 rightValue)
    {
        return new SerializableVector2(leftValue.x + rightValue.x, leftValue.y + rightValue.y);
    }
}