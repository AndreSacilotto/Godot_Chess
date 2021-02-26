using System;
using Godot;
using static Godot.GD;

public class King : Piece<King>
{
	static King(){
		whiteTexture = LoadPieceTexture('w', "king");
		blackTexture = LoadPieceTexture('b', "king");
	}

	public override Vector2Int[] GetArea(PieceBase[,] board){
		return null;
	}

}