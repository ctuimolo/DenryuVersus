using Godot;

using Utilities;

namespace Enemies
{
	public partial class EnemyBee: EnemyBase
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Shader.Play("idle");

			Animator.CurrentAnimation = "idle";
			Animator.Seek(GD.RandRange(0, Animator.CurrentAnimationLength), true);
			Animator.PlaybackSpeed = Utils.RandomFloat(0.4f, 0.8f); 
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
