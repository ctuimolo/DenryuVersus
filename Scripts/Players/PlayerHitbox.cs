using Dynamics;
using Enemies;
using Godot;
using Players;
using System;

public partial class PlayerHitbox : Hitbox
{
  [Export]
  public Player Parent;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
	{
    AreaEntered += Area2DEntered;
  }

  public void Area2DEntered(Area2D other)
  {
    if (other is EnemyHitbox && Parent.Alive && Parent.Interactable)
    {
      Parent.TakeDamage(1);
      return;
    }

    if (other is EnemyBulletHitbox && Parent.Alive && Parent.Interactable)
    {
      Parent.TakeDamage(1);
      return;
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
	{
	}
}
