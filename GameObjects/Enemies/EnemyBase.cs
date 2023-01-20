using Godot;

namespace Enemies
{
	public partial class EnemyBase : Area2D
	{
		[Export]
		public AnimationPlayer Animator;

		[Export]
		public AnimationPlayer Shader;

		[Export]
		public int Health = 20;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
