using System;
using Godot;

using PlayerInstances;

namespace Enemies
{
	public partial class EnemySpawner : Node2D
	{
		[Export]
		public EnemyInstance EnemyInstance;

		public override void _Ready()
		{

		}

		public override void _Process(double delta)
		{

		}
	}
}