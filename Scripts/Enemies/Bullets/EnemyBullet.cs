using Godot;

using Dynamics;
using Players;

namespace Enemies
{
  public partial class EnemyBullet : CharacterBody2D
  {
    [Export]
    EnemyBulletHitbox Hitbox;

    [Export]
    public AnimationPlayer Animator;

    [Export]
    public Sprite2D Sprite;

    [Export]
    public PlayerInstance PlayerInstance;

    private bool _queueDeactivate;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      ZIndex = Consts.ZORDER_FOREPARTICLE;

      Deactivate();
      
      Animator.Play("idle");

      Hitbox.AreaEntered += Area2DEntered;

    }

    public void Fire(Vector2 fireFrom, Vector2 direction)
    {
      _queueDeactivate = false;
      Visible = true;
      ProcessMode = ProcessModeEnum.Always;
      GlobalPosition = fireFrom;
      Velocity = direction;
    }

    public void Deactivate()
    {
      _queueDeactivate = false;
      Velocity = Vector2.Zero;
      Visible = false;
      ProcessMode = ProcessModeEnum.Disabled;
    }

    public void Area2DEntered(Area2D other)
    {
      if (other is Hitbox)
      {
        //if (((EnemyHitbox)other).Parent.Alive && ((EnemyHitbox)other).Parent.Interactable)
        //{
        //  Collide();
        //}
      }
    }

    public void Collide()
    {
      Velocity = Vector2.Zero;
      _queueDeactivate = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      if (Hitbox.ProcessMode != ProcessModeEnum.Disabled)
      {
        if (GlobalPosition.X + 6 < PlayerInstance.Background.GlobalPosition.X ||
            GlobalPosition.X - 6 > PlayerInstance.Background.GlobalPosition.X + PlayerInstance.Background.Size.X ||
            GlobalPosition.Y + 6 < PlayerInstance.Background.GlobalPosition.Y ||
            GlobalPosition.Y - 6 > PlayerInstance.Background.GlobalPosition.Y + PlayerInstance.Background.Size.Y)
        {
          Deactivate();
        }
      }
    }

    public override void _PhysicsProcess(double delta)
    {
      if (!_queueDeactivate)
      {
        MoveAndSlide();
      }
    }
  }
}
