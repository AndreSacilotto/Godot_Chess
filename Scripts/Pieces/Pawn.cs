using System;
using Godot;
using static Godot.GD;

public class Pawn : Piece<Pawn>
{
	static Pawn(){
		whiteTexture = LoadPieceTexture('w', "pawn");
		blackTexture = LoadPieceTexture('b', "pawn");
	}
	
	public override Vector2Int[] GetArea(PieceBase[,] board){
		return null;
	}

}