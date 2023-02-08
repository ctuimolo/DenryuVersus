using Godot;
using System;

using Utilities;


namespace Enemies
{
	public partial class EnemyBee_Spawner03 : EnemyBee_Spawner
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			EnemySeparation = 65;
			EnemyCount = 5;
			Initialize();

			Vector2 pathScale = Scale;
			Vector2 pathPosition = Position;
			bool mirror = Utils.RandomInt(0, 1) == 1 ? true : false;


			TransformPaths(0.5f, pathPosition, pathScale, mirror);
			RandomizeColor();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			base._Process(delta);
		}
	}
}