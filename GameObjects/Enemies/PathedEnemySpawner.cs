using Godot;
using System.Collections.Generic;

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
		private int _enemySeparationTimer = 100;

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
                Paths[i].Path.AddChild(Enemies[i]);
				Paths[i].Path.SetSpeed(Speed);
            }

			ResetEnemies();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			//ValidateEnemiesInbound();
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

		//private void ValidateEnemiesInbound()
  //      {
		//	foreach (EnemyBase enemy in Enemies)
		//	{

		//		float a = enemy.ToGlobal(enemy.Position).x + 32/2;
		//		float b = (PlayerInstance.Position).x;


		//		if (enemy.ToGlobal(enemy.Position).x + enemy.Sprite.Texture.GetSize().x/2 < PlayerInstance.ToGlobal(PlayerInstance.Background.Position).x ||
		//		enemy.ToGlobal(enemy.Position).x - enemy.Sprite.Texture.GetSize().x/2 > PlayerInstance.ToGlobal(PlayerInstance.Background.Position).x + PlayerInstance.Background.Size.x ||
		//		enemy.ToGlobal(enemy.Position).y + enemy.Sprite.Texture.GetSize().y/2 < PlayerInstance.ToGlobal(PlayerInstance.Background.Position).y + PlayerInstance.Background.Size.y)
		//		{
		//			enemy.Interactable = false;
		//			enemy.Visible = false;
  //              }
		//		else 
		//		{
  //        enemy.Interactable = true;
  //        enemy.Visible = true;
  //      }

  //          }
		//}

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