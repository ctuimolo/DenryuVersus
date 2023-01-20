using Godot;

using Globals;
using Backgrounds;
using Enemies;

namespace Players
{
	public partial class Bullet : CharacterBody2D
	{
		[Export]
		BulletHitbox Hitbox;

		[Export]
		public AnimationPlayer Animator;

		[Export]
		public float BulletSpeed = 650;

		public SpaceBackground Background;
		public BulletManager Manager;
		
		private bool _queueDeactivate;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_queueDeactivate = false;

			Hitbox.AreaEntered += Area2DEntered;
		}

		public void Fire(Vector2 fireFrom)
		{
			_queueDeactivate = false;
			Visible = true;
			Hitbox.ProcessMode = ProcessModeEnum.Always;
			Animator.Stop();
			Animator.Play("fired");
			Position = fireFrom;
		}

		public void Deactivate()
        {
			Velocity = Vector2.Zero;
			Visible = false;
			Hitbox.ProcessMode = ProcessModeEnum.Disabled;
			Animator.Stop();
        }

		public void Area2DEntered(Area2D other)
		{
			if (other is EnemyHitbox)
            {
				Collide();
            }
        }

		public void Collide()
        {
			Animator.Play("hit");

			Velocity = Vector2.Zero;
			_queueDeactivate = true;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(_queueDeactivate && !Animator.IsPlaying())
            {
				Deactivate();
				return;
            }

			if (Hitbox.ProcessMode != ProcessModeEnum.Disabled)
			{

				if ( ToGlobal(Position).y < ToGlobal(Manager.OwningPlayer.Background.Position).y - Manager.OwningPlayer.Background.Size.y/2)
				{
					Deactivate();
				}

				Velocity = new Vector2(0, -BulletSpeed);
			}
		}

        public override void _PhysicsProcess(double delta)
        {
			MoveAndSlide();
		}
	}
}
