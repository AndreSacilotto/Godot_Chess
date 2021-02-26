using System;
using Godot;
using static Godot.GD;

public class Rook : Piece<Rook>
{
	static Rook(){
		whiteTexture = LoadPieceTexture('w', "rook");
		blackTexture = LoadPieceTexture('b', "rook");
	}

	public override void _Ready()
	{
		Print(IsWhiteTeam());
		Texture = IsWhiteTeam() ? whiteTexture : blackTexture;
	}

}