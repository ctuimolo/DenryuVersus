using Godot;
using System;

namespace Enemies
{
	public partial class EnemyPath : Path2D
	{
		[Export]
		public EnemyPathFollow Path;

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