using Godot;
using System;
using Players;

using World;

public partial class TitleScreen : Node2D
{
  Timer AnimationTimer { get; set; }

  [Export]
  AnimationPlayer AnimationPlayer { get; set; }

  [Export]
  PackedScene ShmupStage;

  private int _state;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    AnimationTimer = new Timer();
    AnimationTimer.OneShot = true;
    AnimationTimer.Timeout += new Action(StartAnimations);
    AddChild(AnimationTimer);
    AnimationTimer.Start(3);

    _state = 0;
  }

  public void StartAnimations()
  {
    AnimationPlayer.Play("1");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }

  public void ChangeScene()
  {
    GetTree().ChangeSceneToPacked(ShmupStage);
  }

  public override void _Input(InputEvent @event)
  {
    base._Input(@event);

    if (@event.IsActionPressed(Player.PlayerOneInputMap.Button1))
    {
      if (_state == 0)
      {
        WorldManager.Player1_DeviceNumber = @event.Device;
        _state++;
        AnimationPlayer.Play("2");
        return;
      }

      if (_state == 1)
      {
        WorldManager.Player2_DeviceNumber = @event.Device;
        AnimationPlayer.Play("3");
        return;
      }
    }
  }
}
