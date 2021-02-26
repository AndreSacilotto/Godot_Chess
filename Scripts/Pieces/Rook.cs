using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class Rook : Piece<Rook>
{
	static Rook(){
		whiteTexture = LoadPieceTexture('w', "rook");
		blackTexture = LoadPieceTexture('b', "rook");
	}


	public override void GetMoveArea(ICollection<Vector2Int> collection)
	{
	}
	public override void GetKillArea(ICollection<Vector2Int> collection)
	{
	}

}