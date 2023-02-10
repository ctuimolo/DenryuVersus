using System;
using Godot;


using Players;

namespace Enemies
{
	public partial class EnemyManager : Node2D
	{
		[Export]
		public PackedScene[] BeeSpawnersPackages;

		[Export]
		public PlayerInstance PlayerInstance;

		[Export]
		private Timer EnemyTimer;

		private void SpawnBees()
    {
			foreach (PackedScene package in BeeSpawnersPackages)
			{
				EnemyInstance beeSpawner = package.Instantiate<EnemyInstance>();
				beeSpawner.PlayerInstance = PlayerInstance;
				AddChild(beeSpawner);
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