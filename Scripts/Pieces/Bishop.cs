using System;
using Godot;
using static Godot.GD;

public class Bishop : Piece<Bishop>
{
	static Bishop(){
		whiteTexture = LoadPieceTexture('w', "bishop");
		blackTexture = LoadPieceTexture('b', "bishop");
	}

	public override void _Ready()
	{
		Print(IsWhiteTeam());
		Texture = IsWhiteTeam() ? whiteTexture : blackTexture;
	}
}