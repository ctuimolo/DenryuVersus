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
		Visible = false;

		AnimationTimer = new Timer();
		AnimationTimer.OneShot = true;
		AnimationTimer.Timeout += new Action(PlayAnimationFadeIn);
		AddChild(AnimationTimer);
		AnimationTimer.Start(2);
	}

	private void PlayAnimationFadeIn()
  {
		AnimationPlayer.Play("fade in");

		AnimationTimer = new Timer();
		AnimationTimer.OneShot = true;
		AnimationTimer.Timeout += new Action(PlayAnimationSpark);
		AddChild(AnimationTimer);
		AnimationTimer.Start(1);
	}

	private void PlayAnimationSpark()
	{
    AnimationPlayer.Play("spark");
  }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(AnimationPlayer.AssignedAnimation == "spark" && !AnimationPlayer.IsPlaying())
		{
			AnimationPlayer.Play("idle");
		}
	}


}
