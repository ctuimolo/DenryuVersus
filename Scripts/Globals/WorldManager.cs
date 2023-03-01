using Godot;
using Players;
using System;
using System.Runtime.CompilerServices;

namespace World
{
  public partial class WorldManager : Node2D
  {

    public static int Player1_DeviceNumber = 0;
    public static int Player2_DeviceNumber = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {
      base._Input(@event);
      if (@event.IsActionPressed("debug1"))
      {
        foreach (Node child in GetChildren())
        {
          child.QueueFree();
        }

        GetTree().ChangeSceneToFile("res://Scenes/title_screen.tscn");
      }

      if (@event.IsActionPressed("debug2"))
      {
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
        {
          DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
        else
        {
          DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
      }
    }
  }
}
