using Godot;
using System;

using Utilities;

namespace Enemies
{
	public partial class EnemyBee_Spawner01 : EnemyBee_Spawner
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			EnemySeparation = 60;
			EnemyCount = 5;
			Initialize();

			Vector2 pathScale		 = Scale;
			Vector2 pathPosition = Position;
			bool mirror = Utils.RandomInt(0, 1) == 1? true : false;

			pathScale = new Vector2(1, Utils.RandomFloat(0.6f, 1f));
			pathPosition += new Vector2(0, Utils.RandomFloat(-60, 60));

			TransformPaths(0.6f, pathPosition, pathScale, mirror);
			RandomizeColor();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			base._Process(delta);
		}
	}
}