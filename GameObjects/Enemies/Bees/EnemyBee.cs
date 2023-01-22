using Godot;

using Utilities;
using Particles;

namespace Enemies
{
	public partial class EnemyBee: EnemyBase
	{
		[Export]
		private EnemyHitbox Hitbox;

		[Export]
		private Explosion Explosion;

		private bool _dieAfterAnimation = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Shader.Play("idle");

			Animator.CurrentAnimation = "idle";
			Animator.Seek(GD.RandRange(0, Animator.CurrentAnimationLength), true);
			Animator.PlaybackSpeed = Utils.RandomFloat(0.4f, 0.8f);

			Explosion.Visible = false;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			base._Process(delta);

			if (_dieAfterAnimation && !Animator.IsPlaying())
            {
				QueueDeath = true;
            }
		}

		public override void TakeDamage(int damage)
		{
			Health -= damage;

            if (Health <= 0)
            {
                _dieAfterAnimation = true;
                Shader.Play("idle");
                Animator.Play("die");
                Velocity = Vector2.Zero;
				Explosion.Visible = true;
				Explosion.Animator.Play("idle");
            }
        }
	}
}
