using Godot;
using System;

using Utilities;

namespace Enemies
{
	public partial class EnemyBee_Spawner : PathedEnemySpawner
	{

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			RandomizeColor();
		}

		private void RandomizeColor()
    {
			float red = Utils.RandomFloat(150, 255)/255;
			float green = Utils.RandomFloat(150, 255)/255;
			float blue = Utils.RandomFloat(150, 255)/255;

			Color color = new Color(red, green, blue);

			foreach (Enemy bee in Enemies)
      {
				bee.Sprite.SelfModulate = color;
			}				
    }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			base._Process(delta);
		}
	}
}