using System;
using Godot;


using PlayerInstances;

namespace Enemies
{
	public partial class EnemyManager : Node2D
	{
		[Export]
		public PackedScene[] BeeSpawners;

		[Export]
		public PlayerInstance PlayerInstance;

		[Export]
		private Timer EnemyTimer;

		private void SpawnBees()
    {
			foreach (PackedScene beePackage in BeeSpawners)
			{
				EnemyBeeSpawner bee = beePackage.Instantiate<EnemyBeeSpawner>();
				bee.PlayerInstance = PlayerInstance;
				AddChild(bee);
			}
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			EnemyTimer.Timeout += new Action(SpawnBees);
			SpawnBees();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}
	}
}