using System;
using Godot;

public struct Vector2Int : IEquatable<Vector2Int>
{
	public int x;
	public int y;
	public Vector2Int(Vector2Int vec)
	{
		this.x = vec.x;
		this.y = vec.y;
	}
	public Vector2Int(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public void Set(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static Vector2Int operator +(Vector2Int left, int sum) => new Vector2Int(left.x + sum, left.y + sum);
	public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new Vector2Int(left.x + right.x, left.y + left.y);
	public static Vector2Int operator -(Vector2Int vec) => new Vector2Int(-vec.x, -vec.y);
	public static Vector2Int operator -(Vector2Int left, int neg) => new Vector2Int(left.x + neg, left.y + neg);
	public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new Vector2Int(left.x - right.x, left.y - right.y);
	public static Vector2Int operator *(Vector2Int left, Vector2Int right) => new Vector2Int(left.x * right.x, left.y * right.y);
	public static Vector2Int operator *(int scale, Vector2Int vec) => new Vector2Int(vec.x * scale, vec.y * scale);
	public static Vector2Int operator *(Vector2Int vec, int scale) => new Vector2Int(vec.x * scale, vec.y * scale);
	public static Vector2 operator /(Vector2Int left, float divisor) => new Vector2(left.x / divisor, left.y / divisor);
	public static Vector2 operator /(Vector2Int left, Vector2Int divisorv) => new Vector2(left.x / divisorv.x, left.y / divisorv.y);
	public static Vector2 operator %(Vector2Int left, float divisor) => new Vector2(left.x % divisor, left.y % divisor);
	public static Vector2 operator %(Vector2Int left, Vector2Int divisorv) => new Vector2(left.x % divisorv.x, left.y % divisorv.y);

	public static implicit operator Vector2(Vector2Int vec) => new Vector2(vec.x, vec.y);

	public static bool operator ==(Vector2Int left, Vector2Int right)
	{
		var x = left.x == right.x;
		var y = left.y == right.y;
		return x && y;
	}
	public static bool operator !=(Vector2Int left, Vector2Int right)
	{
		var x = left.x != right.x;
		var y = left.y != right.y;
		return x && y;
	}

	public override int GetHashCode()
	{
		return y.GetHashCode() ^ x.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (obj is Vector2Int)
			return Equals((Vector2Int)obj);
		return false;
	}

	public bool Equals(Vector2Int other)
	{
		return x == other.x && y == other.y;
	}

	public override string ToString()
	{
		return String.Format("({0}, {1})", new object[]
		{
			this.x.ToString(),
			this.y.ToString()
		});
	}

}