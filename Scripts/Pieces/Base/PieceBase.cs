using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public abstract class PieceBase : Sprite, ITeam
{
	[Export] public bool IsWhiteTeam { get; protected set; }

	Vector2Int indexPosition;

	public bool IsDead { get; set; } = false;
	public BoardTileMap Board { get; protected set; }

	public Vector2Int IndexPosition
	{
		get => indexPosition;
		set
		{
			indexPosition = value;
			Position = Board.CellCenterToWorld(value);
		}
	}

	protected static Texture LoadPieceTexture(char pieceColor, string pieceName) =>
		Load($"res://Assets/Chess_Pieces/{pieceColor}_{pieceName}_128.png") as Texture;

	public void Setup(bool isWhiteTeam, BoardTileMap board, Vector2Int index)
	{
		IsWhiteTeam = isWhiteTeam;
		Board = board;
		IndexPosition = index;
		IsDead = false;
	}

	public static Vector2Int IndexDeadPosition => new Vector2Int(-1, -1);
	public bool SameTeam(ITeam other) => IsWhiteTeam == other.IsWhiteTeam;

	public List<Vector2Int> GetMoveArea()
	{
		var list = new List<Vector2Int>();
		GetMoveArea(list);
		return list;
	}
	public List<Vector2Int> GetKillArea()
	{
		var list = new List<Vector2Int>();
		GetKillArea(list);
		return list;
	}

	protected Texture WhiteBlackTexture(Texture whiteTex, Texture blackTex) => IsWhiteTeam ? whiteTex : blackTex;
	protected Vector2Int GetPieceYDir() => IsWhiteTeam ? new Vector2Int(0, -1) : new Vector2Int(0, 1);

	public abstract void GetMoveArea(ICollection<Vector2Int> collection);
	public abstract void GetKillArea(ICollection<Vector2Int> collection);

	public virtual void MoveOrKill(Vector2Int start, Vector2Int end)
	{
		var startPiece = Board.GetPiece(start);
		var endPiece = Board.GetPiece(end);
		if (endPiece != null)
			Board.MoveAndKillPiece(startPiece, endPiece);
		else
			Board.MovePiece(startPiece, end);
	}

}
