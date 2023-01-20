using Godot;

using Globals;

namespace Enemies
{
	public abstract partial class EnemyBase : CharacterBody2D, IDamageable
	{
		[Export]
		public AnimationPlayer Animator;

		[Export]
		public AnimationPlayer Shader;

		[Export]
		public int Health = 20;

		public bool Alive { get; set; } = true;
		public bool QueueDeath { get; set; } = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (QueueDeath)
            {
				QueueFree();
				ProcessMode = ProcessModeEnum.Disabled;
            }
		}

		public override void _PhysicsProcess(double delta)
		{
			if(Alive)
            {
				MoveAndSlide();
			}
		}

		public abstract void TakeDamage(int damage);
	}
}
