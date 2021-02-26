using System;
using Godot;
using static Godot.GD;

public class Rook : Piece<Rook>
{
	static Rook(){
		whiteTexture = LoadPieceTexture('w', "rook");
		blackTexture = LoadPieceTexture('b', "rook");
	}


	public override Vector2Int[] GetArea(PieceBase[,] board){
		return null;
	}

}