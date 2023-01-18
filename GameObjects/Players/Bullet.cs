using Godot;

using Backgrounds;

namespace Players
{
	public partial class Bullet : Area2D
	{
		[Export]
		AnimationPlayer Animator;

		[Export]
		public float BulletSpeed = 6;

		public bool Active = false;

		public SpaceBackground Background;

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

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (Active)
			{
				Position += new Vector2(0, -BulletSpeed);
			}
		}
	}
}
