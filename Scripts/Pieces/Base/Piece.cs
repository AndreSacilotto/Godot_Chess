using System;
using Godot;
using static Godot.GD;

public abstract class Piece<T> : PieceBase
{
	protected static Texture whiteTexture;
	protected static Texture blackTexture;

	public override void _EnterTree(){
		Texture = WhiteBlackTexture(whiteTexture, blackTexture); 
		Scale = new Vector2(.5f, .5f);
	}
}
