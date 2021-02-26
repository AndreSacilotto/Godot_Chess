using System;
using Godot;
using static Godot.GD;

[Tool]
public class BoardTileMap : Node2D
{
	public const int width = 8;
	public const int height = 8;

	[Export] public int tileSize = 32;

	[Export] private Color evenColor = new Color(0,0,0);
	[Export] private Color oddColor = new Color(1,1,1);

	public Vector2[,] position = new Vector2[width, height];
	public Color[,] board = new Color[width, height];
	
	public PieceBase[,] black = new PieceBase[width, height];
	public PieceBase[,] white = new PieceBase[width, height];
	
	public override void _Ready()
    {
		Update();
    }

	public override void _Process(float delta)
	{
		
	}

	public override void _Draw()
	{
		var tileDiagonal = new Vector2(tileSize, -tileSize);
		for (int x = 0; x < width; x++)
		{
			var w = x * tileDiagonal.x;
			for (int y = 0; y < height; y++)
			{
				var h = y * tileDiagonal.y;
				var points = new Vector2[]{
					new Vector2(w, h),
					new Vector2(w, h + tileDiagonal.y),
					new Vector2(w + tileDiagonal.x, h + tileDiagonal.y),
					new Vector2(w + tileDiagonal.x, h),
				};
				Color c = (x + y) % 2 == 0 ? evenColor : oddColor;
				DrawColoredPolygon(points, c);
			}
		}
	}

}