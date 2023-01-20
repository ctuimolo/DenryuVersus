using Godot;

using Players;
using Dynamics;
using Utilities;

namespace Enemies
{
	public partial class EnemyBee: EnemyBase
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			//Animator.Play("idle");
			Shader.Play("idle");

			Animator.CurrentAnimation = "idle";
			Animator.Seek(GD.RandRange(0, Animator.CurrentAnimationLength), true);
			Animator.PlaybackSpeed = Utils.RandomFloat(0.4f, 0.8f); 

			AreaEntered += Area2DEntered;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void Area2DEntered(Area2D other)
		{
			if(other is BulletHitbox)
            {
				TakeBullet(other as BulletHitbox);
            }
		}

		private void TakeBullet(BulletHitbox bullet)
        {
			if (!bullet.Active) return;

			Shader.Play("hit");

		}
	}
}
