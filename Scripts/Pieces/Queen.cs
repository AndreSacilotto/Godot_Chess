using System;
using Godot;
using static Godot.GD;

public class Queen : Piece<Queen>
{
	static Queen(){
		whiteTexture = LoadPieceTexture('w', "queen");
		blackTexture = LoadPieceTexture('b', "queen");
	}


	public override Vector2Int[] GetArea(PieceBase[,] board){
		return null;
	}

}