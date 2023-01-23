using Godot;
using System.Collections.Generic;

namespace Enemies
{
	public partial class PathedEnemySpawner : Node2D
	{
		[Export]
		public PackedScene PathPackage;

		[Export]
		public PackedScene EnemyPackage;

		[Export]
		public int EnemyCount = 5;

		[Export]
		public int EnemySeparation = 100;
		private int _enemySeparationTimer = 100;

		public List<EnemyBase> Enemies = new List<EnemyBase>();
		public List<EnemyPath> Paths = new List<EnemyPath>();

		private int _enemyIdx = 0;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
            for (int i = 0; i < EnemyCount; i++)
            {
                Enemies.Add(EnemyPackage.Instantiate<EnemyBee>());
                Paths.Add(PathPackage.Instantiate<EnemyPath>());

                AddChild(Paths[i]);
                Paths[i].Path.AddChild(Enemies[i]);
            }

			ResetEnemies();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(CheckAllEnemiesDead())
            {
				ResetEnemies();
            }				
			TrySpawnEnemies();
		}

		private void ResetEnemies()
        {
			for (int i = 0; i < EnemyCount; i++)
			{
				Enemies[i].Alive = false;
				Paths[i].Path.Progress = 0;
				Paths[i].Path.Alive = false;

				_enemyIdx = 0;
				_enemySeparationTimer = 0;
			}
		}

		private bool CheckAllEnemiesDead()
        {
			foreach(EnemyBase enemy in Enemies)
            {
				if(enemy.Alive)
                {
					return false;
                }
            }
			return true;
        }

		private void TrySpawnEnemies()
        {
			if (_enemySeparationTimer == 0)
			{
				if (!Paths[_enemyIdx].Path.Alive && !Enemies[_enemyIdx].Alive)
				{
					Paths[_enemyIdx].Path.Alive = true;
					Enemies[_enemyIdx].MakeAlive();

					_enemyIdx++;
					if (_enemyIdx >= EnemyCount)
					{
						_enemyIdx = 0;
					}
				}
			}
			_enemySeparationTimer++;
			if (_enemySeparationTimer >= EnemySeparation)
			{
				_enemySeparationTimer = 0;
			}
		}
	}
}