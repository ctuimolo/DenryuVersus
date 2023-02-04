using Godot;
using Utilities;

using PlayerInstances;

namespace Enemies
{
  public partial class EnemyInstance : Node2D
  {
    [Export]
    public PackedScene EnemyPackage;

    [Export]
    public EnemySpawner EnemySpawner;

    [Export]
    public PlayerInstance PlayerInstance;

    public Enemy Enemy;

    public override void _Ready()
    {
      base._Ready();
    }

    public override void _Process(double delta)
    {
      base._Process(delta);
    }
  }
}
