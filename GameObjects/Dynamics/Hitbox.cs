using Godot;

namespace Dynamics
{
	public partial class Hitbox : Area2D
	{
		[Export]
		public Node2D Owner;

		[Export]
		public bool Active = true;
		
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