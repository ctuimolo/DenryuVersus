using Godot;

using Utilities;

namespace Enemies
{
	public partial class EnemyBee: EnemyBase
	{
		[Export]
		private EnemyHitbox Hitbox;

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
			base._Process(delta);
			//Velocity = new Vector2(Utils.RandomFloat(-200, 200), Utils.RandomFloat(-200, 200));
		}

		public override void TakeDamage(int damage)
        {
			Health -= damage;
			
			if(Health <= 0)
            {
				QueueDeath = true;
				Shader.Play("idle");
				Animator.Play("die");
				Velocity = Vector2.Zero;
            }
        }
	}
}
