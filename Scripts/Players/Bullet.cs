using Backgrounds;
using Enemies;
using Godot;
using World;

namespace Players
{
  public partial class Bullet : CharacterBody2D
  {
    [Export]
    BulletHitbox Hitbox;

    [Export]
    public AnimationPlayer Animator;

    [Export]
    public Sprite2D Sprite;

    [Export]
    public float BulletSpeed = 1000;

    public SpaceBackground Background;
    public BulletManager Manager;

    private bool _queueDeactivate;
    private WorldManager _worldManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");

      _queueDeactivate = false;

      Hitbox.AreaEntered += Area2DEntered;
    }

    public void Fire(Vector2 fireFrom)
    {
      _queueDeactivate = false;
      Visible = true;
      Hitbox.ProcessMode = ProcessModeEnum.Always;
      Animator.Play("fired");
      Position = fireFrom;
    }

    public void Deactivate()
    {
      Velocity = Vector2.Zero;
      Visible = false;
      Hitbox.ProcessMode = ProcessModeEnum.Disabled;
      _worldManager.RemoveChild(this);
    }

    public void Area2DEntered(Area2D other)
    {
      if (other is EnemyHitbox)
      {
        if (((EnemyHitbox)other).Parent.Alive && ((EnemyHitbox)other).Parent.Interactable)
        {
          Collide();
        }
      }
    }

    public void Collide()
    {
      Animator.Play("hit");

      Velocity = Vector2.Zero;
      _queueDeactivate = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      if (_queueDeactivate && !Animator.IsPlaying())
      {
        Deactivate();
        return;
      }

      if (Hitbox.ProcessMode != ProcessModeEnum.Disabled)
      {

        if (GlobalPosition.Y < Manager.OwningPlayer.Background.GlobalPosition.Y - Manager.OwningPlayer.Background.Size.Y / 2 - Utilities.Utils.GetSpriteLiteralSize(Sprite).Y)
        {
          Deactivate();
        }

        Velocity = new Vector2(0, -BulletSpeed);
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
