using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class Player : Node2D, ITeam
{
	public int playerID;

	public bool IsWhiteTeam { get; private set; }

	public Player(bool isWhiteTeam)
	{
		this.IsWhiteTeam = isWhiteTeam;
	}

	Vector2Int start;

	PieceBase holding;
	public PieceBase Holding
	{
		get => holding;
		set
		{
			if(!value.SameTeam(this))
				return;
			holding = value;
			MovesList = value.GetMoveArea();
			KillList = value.GetKillArea();
			start = value.IndexPosition;
			RenderMoveList();
		}
	}

	public List<Vector2Int> MovesList { get; private set; }
	public List<Vector2Int> KillList { get; private set; }

	public void RenderMoveList(){
		//holding.Board.
	}

	public bool AllowedMove(Vector2Int vec) 
	{
		foreach (var el in MovesList)
			if (el == vec)
				return true;
		foreach (var el in KillList)
			if (el == vec)
				return true;
		return false;
	}

	public void MakeMove(){
		var mousePos = GetGlobalMousePosition();
		var end = holding.Board.WorldToCell(mousePos);
		if (AllowedMove(end))
			holding.MoveOrKill(start, end);
	}

}
