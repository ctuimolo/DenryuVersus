using Godot;
using System;

public partial class Frame : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ZIndex = Consts.ZORDER_UIFORE;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
