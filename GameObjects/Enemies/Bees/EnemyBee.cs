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

		private Vector2 _velMaxPos = new Vector2(100,   30);
		private Vector2 _velMaxNeg = new Vector2( 10, -500);
		private int _currentHealth;

		float t;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

        public override void MakeAlive()
        {
            base.MakeAlive();

			Shader.Play("idle");

			Animator.CurrentAnimation = "idle";
			Animator.Seek(GD.RandRange(0, Animator.CurrentAnimationLength), true);
			Animator.PlaybackSpeed = Utils.RandomFloat(0.4f, 0.8f);

			Explosion.Visible = false;
			_currentHealth = Health;
			_dieAfterAnimation = false;
		}

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
		{
			base._Process(delta);

			if (_dieAfterAnimation && !Animator.IsPlaying() && !Explosion.Animator.IsPlaying())
            {
				QueueDeath = true;
            }
		}

		public override void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			Shader.Play("hit");

            if (_currentHealth <= 0)
            {
				Alive = false;
                _dieAfterAnimation = true;
				IsDying = true;
                Shader.Play("idle");
                Animator.Play("die");
                Velocity = Vector2.Zero;
				Explosion.Visible = true;
				Explosion.Animator.Play("idle");
            }
        }
	}
}
