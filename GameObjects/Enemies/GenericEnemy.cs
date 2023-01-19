using Godot;
using System;

public partial class GenericEnemy : Sprite2D
{
	[Export]
	public AnimationPlayer Animator;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Animator.Play("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
