using System;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

public class BoardTileMap : Node2D
{
	public const int width = 8;
	public const int height = 8;

	[Export] public int tileSize = 64;
	float half;

	[Export] private Color evenColor = new Color(0, 0, 0);
	[Export] private Color oddColor = new Color(1, 1, 1);

	public PieceBase[,] boardPieces = new PieceBase[width, height];
	public List<PieceBase> white = new List<PieceBase>();
	public List<PieceBase> black = new List<PieceBase>();

	private Node2D whiteCemetery, blackCemetery;
	private Node2D piecesHolder;

	public override void _Ready()
	{
		half = tileSize * .5f;

		Position = new Vector2(-width * half, -height * half);
		piecesHolder = GetNode<Node2D>("/root/Main/Pieces");
		whiteCemetery = new Position2D();
		whiteCemetery.Name = nameof(whiteCemetery);
		whiteCemetery.Position = new Vector2(Position.x - tileSize * 1.5f, -(Position.y + tileSize * .25f));
		AddChild(whiteCemetery);

		blackCemetery = new Position2D();
		blackCemetery.Name = nameof(blackCemetery);
		blackCemetery.Position = -whiteCemetery.Position;
		AddChild(blackCemetery);

		var rook = new Rook();
		var rook2 = new Rook();
		rook.Position = blackCemetery.Position;
		piecesHolder.AddChild(rook);

		rook2.IsWhiteTeam = true;
		rook2.Position = whiteCemetery.Position;
		piecesHolder.AddChild(rook2);

		AddPiece<Queen>(1, 1, true);
		AddPiece<Queen>(4, 4, false);
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

	public static bool IsInside(int x, int y) =>
		x >= 0 && y >= 0 && x < width && y < height;
	public bool IsOccupied(int x, int y) => boardPieces[x, y] != null;


	public void WorldToCell(Vector2 worldPos, out int x, out int y)
	{
		x = Mathf.FloorToInt(worldPos.x / -tileSize);
		y = Mathf.FloorToInt(worldPos.y / -tileSize);
	}
	public Vector2 CellIndexToWorld(Vector2Int index) => Position + index * tileSize;
	public Vector2 CellCenterToWorld(Vector2Int index)
	{
		float x = Position.x + tileSize * (index.x + .5f);
		float y = Position.y + tileSize * (index.y + .5f);
		return new Vector2(x, y);
	}


	public void MovePiece(int x0, int y0, int x1, int y1){
		if(!IsInside(x0, y0))
			return;
		var movePiece =	boardPieces[x0, y0];
		boardPieces[x0, y0] = null;
		var destiny = boardPieces[x1, y1];
		if (destiny != null){
			PieceToCemetery(destiny);
			boardPieces[x1, y1] = null;
		}
		boardPieces[x1, y1] = movePiece;
	}
	

	public void AddPiece<T>(int x, int y, bool IsWhiteTeam) where T : PieceBase, new()
	{
		var valid = IsInside(x, y) && !IsOccupied(x, y);

		if (valid){
			var piece = new T();
			piece.SetAll(x, y, IsWhiteTeam);
			piece.Position = CellCenterToWorld(piece.IndexPosition);
			piecesHolder.AddChild(piece);
		}
	}

	public void PieceToCemetery(PieceBase piece)
	{
		piece.IsDead = true;
		piece.SetIndexDeadPosition();
		if (piece.IsWhiteTeam)
		{
			var newPos = new Vector2(whiteCemetery.Position.x, whiteCemetery.Position.y + half);
			piece.Position = whiteCemetery.Position;
			whiteCemetery.Position = newPos;
		}
		else
		{
			var newPos = new Vector2(blackCemetery.Position.x, blackCemetery.Position.y - half);
			piece.Position = blackCemetery.Position;
			blackCemetery.Position = newPos;
		}
	}

}