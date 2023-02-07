using Godot;
using Players;
using System.Collections.Generic;
using Utilities;

namespace Enemies
{
  public partial class PathedEnemySpawner : EnemySpawner
  {
    [Export]
    public PackedScene PathPackage;

    [Export]
    public int EnemyCount = 4;

    [Export]
    public int EnemySeparation = 100;
    private int _enemySeparationTimer = 0;

    [Export]
    public float Speed = 0.5f;

    public List<Enemy> Enemies = new List<Enemy>();
    public List<EnemyPath> Paths = new List<EnemyPath>();

    private int _enemyIdx = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      for (int i = 0; i < EnemyCount; i++)
      {
        Enemies.Add(EnemyInstance.EnemyPackage.Instantiate<Enemy>());
        Enemies[i].EnemyInstance = EnemyInstance;
        AddChild(Enemies[i]);

        Paths.Add(PathPackage.Instantiate<EnemyPath>());
        Paths[i].Path.Enemy = Enemies[i];
        Paths[i].Path.SetSpeed(Speed);
        AddChild(Paths[i]);
      }

      ResetEnemies();
      TrySpawnEnemies();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      if (Enemies.Count > 0 && Paths.Count > 0)
      {
        MoveEnemies();

        ValidateEnemiesInbound();

        if (CheckAllEnemiesDead())
        {
          DeleteThis();
          return;
        }

        if (CheckAllEnemiesFinishedPath())
        {
          DeleteThis();
          return;
        }

        TrySpawnEnemies();
      }
    }

    private void DeleteThis()
    {
      QueueFree();
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

    private void MoveEnemies()
    {
      foreach (EnemyPath path in Paths)
      {
        path.Path.Enemy.GlobalPosition = path.Path.GlobalPosition;
      }
    }

    private void ValidateEnemiesInbound()
    {
      foreach (Enemy enemy in Enemies)
      {
        Vector2 enemySize = Utils.GetSpriteLiteralSize(enemy.Sprite);

        if (enemy.GlobalPosition.X + enemySize.X / 2 < EnemyInstance.PlayerInstance.Background.GlobalPosition.X ||
            enemy.GlobalPosition.X - enemySize.X / 2 > EnemyInstance.PlayerInstance.Background.GlobalPosition.X + EnemyInstance.PlayerInstance.Background.Size.X ||
            enemy.GlobalPosition.Y + enemySize.Y / 2 < EnemyInstance.PlayerInstance.Background.GlobalPosition.Y ||
            enemy.GlobalPosition.Y - enemySize.Y / 2 > EnemyInstance.PlayerInstance.Background.GlobalPosition.Y + EnemyInstance.PlayerInstance.Background.Size.Y)
        {
          enemy.Visible = false;
          enemy.Interactable = false;
        } else
        {
          enemy.Visible = true;
          enemy.Interactable = true;
        }
      }
    }

    private bool CheckAllEnemiesDead()
    {
      foreach (Enemy enemy in Enemies)
      {
        if (enemy.Alive || enemy.IsDying)
        {
          return false;
        }
      }
      return true;
    }

    private bool CheckAllEnemiesFinishedPath()
    {
      foreach (EnemyPath path in Paths)
      {
        if (path.Path.ProgressRatio < 1)
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