using Godot;
using System.Collections.Generic;

using Utilities;
using PlayerInstances;

namespace Enemies
{
	public partial class PathedEnemySpawner : Node2D
	{
		[Export]
		PlayerInstance PlayerInstance;

		[Export]
		public PackedScene PathPackage;

		[Export]
		public PackedScene EnemyPackage;

		[Export]
		public int EnemyCount = 4;

		[Export]
		public int EnemySeparation = 100;
		private int _enemySeparationTimer = 0;

		[Export]
		public float Speed = 0.5f;

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
				Paths[i].Path.Enemy = Enemies[i];
                Paths[i].Path.AddChild(Enemies[i]);
				Paths[i].Path.SetSpeed(Speed);
            }

			ResetEnemies();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (Enemies.Count > 0 && Paths.Count > 0)
			{
				ValidateEnemiesInbound();

				if (CheckAllEnemiesDead())
				{
					ResetEnemies();
				}

                TrySpawnEnemies();
            }
		}

		private void ResetEnemies()
        {
			for (int i = 0; i < EnemyCount; i++)
			{
				Paths[i].Path.Progress = 0;
				_enemySeparationTimer = 0;
				_enemyIdx = 0;
			}
		}

        private void ValidateEnemiesInbound()
        {
			foreach (EnemyBase enemy in Enemies)
			{
				Vector2 enemySize = Utils.GetSpriteLiteralSize(enemy.Sprite);

				if (enemy.GlobalPosition.x + enemySize.x/2 < PlayerInstance.Background.GlobalPosition.x ||
					enemy.GlobalPosition.x - enemySize.x/2 > PlayerInstance.Background.GlobalPosition.x + PlayerInstance.Background.Size.x ||
					enemy.GlobalPosition.y + enemySize.y/2 < PlayerInstance.Background.GlobalPosition.y ||
					enemy.GlobalPosition.y - enemySize.y/2 > PlayerInstance.Background.GlobalPosition.y + PlayerInstance.Background.Size.y)
				{
					enemy.Visible = false;
					enemy.Interactable = false;
				}
				else
				{
					enemy.Visible = true;
					enemy.Interactable = true;
				}
			}
		}

        private bool CheckAllEnemiesDead()
        {
			foreach(EnemyBase enemy in Enemies)
            {
				if(enemy.Alive || enemy.IsDying)
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
				if (!Enemies[_enemyIdx].Alive && !Enemies[_enemyIdx].IsDying && Paths[_enemyIdx].Path.Progress == 0)
				{
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