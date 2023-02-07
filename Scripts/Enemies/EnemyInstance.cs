using Godot;
using Utilities;

using Players;

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
