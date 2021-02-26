using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class Bishop : Piece<Bishop>
{
	static Bishop(){
		whiteTexture = LoadPieceTexture('w', "bishop");
		blackTexture = LoadPieceTexture('b', "bishop");
	}

	public override void GetMoveArea(ICollection<Vector2Int> collection)
	{
	}
	public override void GetKillArea(ICollection<Vector2Int> collection)
	{
	}
}