using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class Queen : Piece<Queen>
{
	static Queen(){
		whiteTexture = LoadPieceTexture('w', "queen");
		blackTexture = LoadPieceTexture('b', "queen");
	}


	public override void GetMoveArea(ICollection<Vector2Int> collection)
	{
	}
	public override void GetKillArea(ICollection<Vector2Int> collection)
	{
	}

}