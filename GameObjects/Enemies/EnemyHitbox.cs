using Godot;

using Players;

namespace Enemies
{
	public partial class EnemyHitbox : Area2D
	{
		[Export]
		public EnemyBase Owner;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			AreaEntered += Area2DEntered;
		}

		public void Area2DEntered(Area2D other)
		{
			if (other is BulletHitbox)
			{
				TakeBullet(other as BulletHitbox);
			}
		}

		private void TakeBullet(BulletHitbox bullet)
		{
			if (!bullet.Active) return;

			Owner.Shader.Play("hit");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}