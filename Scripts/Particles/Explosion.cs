using Godot;

namespace Particles
{
  public partial class Explosion : Sprite2D
  {
    [Export]
    public AnimationPlayer Animator;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
  }
}