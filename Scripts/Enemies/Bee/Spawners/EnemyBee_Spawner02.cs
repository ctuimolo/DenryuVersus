using Godot;
using System;

using Utilities;

namespace Enemies
{
	public partial class EnemyBee_Spawner02 : EnemyBee_Spawner
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			EnemySeparation = 60;
			EnemyCount = 5;
			Initialize();

			Vector2 pathScale = Scale;
      Vector2 pathPosition = Position;
      bool mirror = Utils.RandomInt(0, 1) == 1 ? true : false;

      pathPosition = new Vector2(Utils.RandomFloat(0, EnemyInstance.PlayerInstance.Background.Size.X/2 - 20), 0);

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