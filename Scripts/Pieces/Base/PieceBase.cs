using System;
using Godot;
using static Godot.GD;

public abstract class PieceBase : Sprite
{
	public Vector2Int IndexPosition { get; private set; }

	public bool IsDead { get; set; } = false;

	[Export] public bool IsWhiteTeam { get; set; } 

	public void SetAll(int x, int y, bool IsWhiteTeam){
		IndexPosition = new Vector2Int(x, y);
		this.IsWhiteTeam = IsWhiteTeam;
	}

	protected static Texture LoadPieceTexture(char pieceColor, string pieceName) => 
		Load($"res://Assets/Chess_Pieces/{pieceColor}_{pieceName}_128.png") as Texture;	

	public void SetIndexDeadPosition(){
		IndexPosition = new Vector2Int(-1, -1);
	}
			
	protected Texture WhiteBlackTexture(Texture whiteTex, Texture blackTex) => IsWhiteTeam ? whiteTex : blackTex;

	public abstract Vector2Int[] GetArea(PieceBase[,] board);
}
