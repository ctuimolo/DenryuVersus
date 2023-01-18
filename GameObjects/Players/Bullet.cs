using Godot;

using Backgrounds;

namespace Players
{
	public partial class Bullet : Area2D
	{
		[Export]
		public AnimationPlayer Animator;

		[Export]
		public float BulletSpeed = 6;
		public bool Active = false;
		public SpaceBackground Background;

		public BulletManager Manager;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Active = false;
		}

		public void Fire(Vector2 fireFrom)
		{
			Visible = true;
			Active	= true;
			Animator.Stop();
			Animator.Play("fired");
			Position = fireFrom;
		}

		public void Deactivate()
        {
			Visible = false;
			Active = false;
			Animator.Stop();
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (Active)
			{
				Position += new Vector2(0, -BulletSpeed);
				if( ToGlobal(Position).y < ToGlobal(Manager.OwningPlayer.Background.Position).y - Manager.OwningPlayer.Background.Size.y/2)
				{
					Deactivate();
				}
			}
		}
	}
}
