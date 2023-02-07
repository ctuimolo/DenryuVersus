using Godot;
using System.Collections.Generic;

using Utilities;

namespace Backgrounds
{
  public partial class SpaceBackground : ColorRect
  {
    [Export]
    PackedScene StarPackage;

    [Export]
    int StarCount = 12;

    [Export]
    int SlowStarCount = 12;

    List<Star> _stars = new List<Star>();
    List<Star> _slowStars = new List<Star>();

    [Export]
    Vector2 StarSpeed = new Vector2(0f, 0.25f);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      for (int i = 0; i < StarCount; i++)
      {
        Star newStar = StarPackage.Instantiate<Star>();

        newStar.Position = new Vector2(Utils.RandomFloat(0, Size.X), Utils.RandomFloat(0, Size.Y));

        _stars.Add(newStar);
        AddChild(newStar);
      }

      for (int i = 0; i < SlowStarCount; i++)
      {
        Star newStar = StarPackage.Instantiate<Star>();

        newStar.Slow = true;
        newStar.Position = new Vector2(Utils.RandomFloat(0, Size.X), Utils.RandomFloat(0, Size.Y));
        _slowStars.Add(newStar);
        AddChild(newStar);
      }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      foreach (Star star in _stars)
      {
        star.Position += StarSpeed;
        if (star.Position.Y > Size.Y)
        {
          star.Position = new Vector2(GD.Randi() % Size.X, 0);
        }
      }

      foreach (Star star in _slowStars)
      {
        star.Position += StarSpeed / 3;
        if (star.Position.Y > Size.Y)
        {
          star.Position = new Vector2(GD.Randi() % Size.X, 0);
        }
      }
    }
  }
}