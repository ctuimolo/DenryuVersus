using Godot;
using System;

public partial class Title : Sprite2D
{
	[Export]
	Timer AnimationTimer { get; set; }

  [Export]
  AnimationPlayer AnimationPlayer { get; set; }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
	{
		AnimationPlayer.Play("fade in");
		AnimationTimer.Start(1);
		AnimationTimer.OneShot = true;

		AnimationTimer.Timeout += new Action(PlayAnimation);
	}

	private void PlayAnimation()
	{
    AnimationPlayer.Play("spark");
  }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!AnimationPlayer.IsPlaying())
		{
			AnimationPlayer.Play("idle");
		}
	}


}
