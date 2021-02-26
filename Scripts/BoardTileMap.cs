using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class BoardTileMap : Node2D
{
	public const int width = 8;
	public const int height = 8;

	[Export] private int tileSize = 64;

	[Export] private Color evenColor = new Color(0, 0, 0);
	[Export] private Color oddColor = new Color(1, 1, 1);
	[Export] private Color dangerColor = new Color(1, 0, 0);
	[Export] private Color moveColor = new Color(0, 1, 0);

	PieceBase[,] boardPieces = new PieceBase[width, height];
	List<PieceBase> whitePieces = new List<PieceBase>();
	List<PieceBase> blackPieces = new List<PieceBase>();

	Node2D whiteCemetery, blackCemetery;
	Node2D piecesHolder;

	#region GODOT FUNCS
	public override void _Ready()
	{
		var half = tileSize * .5f;
		Position = new Vector2(-width * half, -height * half);
		piecesHolder = GetNode<Node2D>("/root/Main/Pieces");
		whiteCemetery = new Node2D(){
			Name = nameof(whiteCemetery),
			Position = new Vector2(Position.x - tileSize * 1.5f, -(Position.y + tileSize * .25f))
		};
		AddChild(whiteCemetery);

		blackCemetery = new Node2D(){
			Name = nameof(blackCemetery),
			Position = -whiteCemetery.Position
		};
		AddChild(blackCemetery);
	}

	public override void _Process(float delta)
	{

	}

	public override void _UnhandledInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton evMouse)
		{
			var mousePos = GetGlobalMousePosition();
			WorldToCell(Position - mousePos, out int x, out int y);
			Print($"{x} : {y}");
		}
	}
	public override void _Draw()
	{
		for (int x = 0; x < width; x++)
		{
			var w = x * tileSize;
			for (int y = 0; y < height; y++)
			{
				var h = y * tileSize;
				var points = new Vector2[]{
					new Vector2(w, h),
					new Vector2(w, h + tileSize),
					new Vector2(w + tileSize, h + tileSize),
					new Vector2(w + tileSize, h),
				};
				Color c = (x + y) % 2 == 0 ? evenColor : oddColor;
				DrawColoredPolygon(points, c);
			}
		}
	}
	#endregion

	public static bool IsInside(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;
	public static bool IsInside(Vector2Int vec) => IsInside(vec.x, vec.y);

	public bool IsOccupied(int x, int y) => boardPieces[x, y] != null;
	public bool IsOccupied(Vector2Int vec) => IsOccupied(vec.x, vec.y);

	#region GET
	public PieceBase GetPiece(int x, int y) => boardPieces[x, y];
	public PieceBase GetPiece(Vector2Int vec) => boardPieces[vec.x, vec.y];

	public PieceBase GetPieceSafe(int x, int y) => IsInside(x, y) ? boardPieces[x, y] : null;
	public PieceBase GetPieceSafe(Vector2Int vec) => IsInside(vec) ? boardPieces[vec.x, vec.y] : null;
	#endregion

	#region SET
	public void SetPiece(int x, int y, PieceBase piece)	{
		boardPieces[x, y] = piece;
		piece.IndexPosition = new Vector2Int(x, y);
		piece.Position = CellCenterToWorld(piece.IndexPosition);
	}

	public void SetPiece(Vector2Int vec, PieceBase piece) => SetPiece(vec.x, vec.y, piece);

	public void SetPieceSafe(int x, int y, PieceBase piece)
	{
		if (IsInside(x, y))
			SetPiece(x, y, piece);
	}
	public void SetPieceSafe(Vector2Int vec, PieceBase piece)
	{
		if (IsInside(vec))
			SetPiece(vec, piece);
	}
	#endregion

	#region WORLD
	public Vector2Int WorldToCell(Vector2 worldPos){
		WorldToCell(worldPos, out int x, out int y);
		return new Vector2Int(x, y);
	}
	public void WorldToCell(Vector2 worldPos, out int x, out int y)
	{
		x = Mathf.FloorToInt(worldPos.x / -tileSize);
		y = Mathf.FloorToInt(worldPos.y / -tileSize);
	}
	public Vector2 CellPivotToWorld(Vector2Int index) => Position + index * tileSize;
	public Vector2 CellCenterToWorld(Vector2Int index)
	{
		float x = Position.x + tileSize * (index.x + .5f);
		float y = Position.y + tileSize * (index.y + .5f);
		return new Vector2(x, y);
	}
	#endregion

	public HashSet<Vector2Int> GetPiecesAreas(IEnumerable<PieceBase> pieces)
	{
		var hash = new HashSet<Vector2Int>();
		return hash;
	}
	public void AddPiece<T>(int x, int y, bool isWhiteTeam) where T : PieceBase, new()
	{
		if (IsInside(x, y) && !IsOccupied(x, y))
		{
			var piece = new T();
			piece.Setup(isWhiteTeam, this, new Vector2Int(x, y));
			piecesHolder.AddChild(piece);
		}
	}

	public void MovePiece(PieceBase piece, Vector2 index)
	{

	}

	public void MoveAndKillPiece(PieceBase killer, PieceBase killed)
	{

	}

	public void PieceToCemetery(PieceBase piece)
	{
		piece.IsDead = true;
		piece.IndexPosition = PieceBase.IndexDeadPosition;
		if (piece.IsWhiteTeam)
		{
			var newPos = new Vector2(whiteCemetery.Position.x, whiteCemetery.Position.y + tileSize * 0.5f);
			piece.Position = whiteCemetery.Position;
			whiteCemetery.Position = newPos;
			whitePieces.Remove(piece);
		}
		else
		{
			var newPos = new Vector2(blackCemetery.Position.x, blackCemetery.Position.y - tileSize * 0.5f);
			piece.Position = blackCemetery.Position;
			blackCemetery.Position = newPos;
			blackPieces.Remove(piece);
		}
	}

}