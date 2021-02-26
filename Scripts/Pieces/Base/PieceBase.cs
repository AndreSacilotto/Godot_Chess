using System;
using Godot;
using static Godot.GD;

public abstract class PieceBase : Sprite
{
	[Export(PropertyHint.Enum, "White, Black")] 
	private int teamColor = 0;

	public bool IsWhiteTeam() => teamColor == (int)TeamColor.white;

	protected static Texture LoadPieceTexture(char pieceColor, string pieceName) => 
		Load($"res://Assets/Chess_Pieces/{pieceColor}_{pieceName}_128.png") as Texture;	
			
	protected Texture WhiteBlackTexture(Texture white, Texture black) => IsWhiteTeam() ? white : black;

	public enum TeamColor{
		white = 0,
		black
	}
}
