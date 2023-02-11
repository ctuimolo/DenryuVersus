using Godot;
using System;

using World;
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
		Player1_Instance.DeviceNumber = WorldManager.Player1_DeviceNumber;
		Player2_Instance.DeviceNumber = WorldManager.Player2_DeviceNumber;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
