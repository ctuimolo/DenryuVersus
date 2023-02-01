using Godot;

using Players;

namespace Enemies
{
	public  partial class EnemyHitbox : Area2D
	{
		[Export]
		public EnemyBase Parent;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			AreaEntered += Area2DEntered;
		}

		public void Area2DEntered(Area2D other)
		{
			if (other is BulletHitbox && Parent.Alive && Parent.Interactable)
			{
				TakeBullet(other as BulletHitbox);
			}
		}

		private void TakeBullet(BulletHitbox bullet)
		{
			Parent.TakeDamage(bullet.Damage);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}