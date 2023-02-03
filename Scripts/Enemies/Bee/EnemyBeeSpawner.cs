using Godot;
using Utilities;

namespace Enemies
{
  public partial class EnemyBeeSpawner : Node
  {
    [Export]
    public PackedScene spawn_01;
    [Export]
    public PackedScene spawn_02;
    [Export]
    public PackedScene spawn_03;
  }
}
