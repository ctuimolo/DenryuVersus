using Godot;

namespace Backgrounds
{
  public partial class Star : ColorRect
  {
    [Export]
    public AnimationPlayer StarAnimator;

    [Export]
    public bool Slow = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      if (Slow)
      {
        StarAnimator.Play("slow twinkle");
      } else
      {
        StarAnimator.Play("twinkle");
      }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
  }
}