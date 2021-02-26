using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class Pawn : Piece<Pawn>
{
	bool HasMoved { get; set; } = false;
	bool MovedTwo { get; set; } = false;

	static Pawn(){
		whiteTexture = LoadPieceTexture('w', "pawn");
		blackTexture = LoadPieceTexture('b', "pawn");
	}
	
	public override void GetMoveArea(ICollection<Vector2Int> collection)
	{
		var yDir = GetPieceYDir();
		var i1 = IndexPosition + yDir;
		if (BoardTileMap.IsInside(i1) && !Board.IsOccupied(i1))
			collection.Add(i1);
		if (!HasMoved){
			var i2 = IndexPosition + yDir * 2;
			if (BoardTileMap.IsInside(i2) && !Board.IsOccupied(i2))
				collection.Add(i2);
		}
	}

	public override void GetKillArea(ICollection<Vector2Int> collection)
	{
		var yDir = GetPieceYDir();

		var i1 = new Vector2Int(IndexPosition.x - 1, IndexPosition.y + yDir.y);
		var p1 = Board.GetPieceSafe(i1);
		if (p1 != null && !SameTeam(p1))
			collection.Add(i1);

		var i2 = new Vector2Int(IndexPosition.x + 1, IndexPosition.y + yDir.y);
		var p2 = Board.GetPieceSafe(i2);
		if (p2 != null && !SameTeam(p2))
			collection.Add(i2);
	}

	public override void MoveOrKill(Vector2Int start, Vector2Int end)
	{
		base.MoveOrKill(start, end);
		MovedTwo = Mathf.Abs(start.y - end.y) >= 2;
		HasMoved = true;
	}
}