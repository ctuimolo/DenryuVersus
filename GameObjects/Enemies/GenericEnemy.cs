using Godot;

using Players;

namespace Enemies
{
	public partial class GenericEnemy : Area2D
	{
		[Export]
		public AnimationPlayer Animator;

		[Export]
		public AnimationPlayer Shader;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Animator.Play("idle");
			Shader.Play("idle");

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
			if (!bullet.GetParent<Bullet>().Active) return;

			Shader.Play("hit");
		}
	}
}
