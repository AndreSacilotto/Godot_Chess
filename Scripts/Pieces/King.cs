using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class King : Piece<King>
{
	static King(){
		whiteTexture = LoadPieceTexture('w', "king");
		blackTexture = LoadPieceTexture('b', "king");
	}

	public override void GetMoveArea(ICollection<Vector2Int> collection)
	{
	}
	public override void GetKillArea(ICollection<Vector2Int> collection)
	{
	}

}