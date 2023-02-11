using Godot;
using System;

using Players;

public partial class GameManager : Node2D
{
	[Export]
	PlayerInstance Player1_Instance;

	[Export]
	PlayerInstance Player2_Instance;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
