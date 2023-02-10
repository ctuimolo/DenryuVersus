using Godot;

namespace Enemies
{
  public abstract partial class Enemy : CharacterBody2D, IDamageable
  {
    [Export]
    public EnemyInstance EnemyInstance;

    [Export]
    public Sprite2D Sprite;

    [Export]
    public AnimationPlayer Animator;

    [Export]
    public AnimationPlayer Shader;

    [Export]
    public int Health = 40;

    public bool Alive { get; set; } = false;
    public bool Interactable { get; set; } = false;
    public bool IsDying { get; set; } = false;
    public bool QueueDeath { get; set; } = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      ZIndex = Consts.ZORDER_ENEMY;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      if (QueueDeath)
      {
        Die();
      }
    }

    private void Die(bool queueFree = false)
    {
      if (queueFree) QueueFree();
      Interactable = false;
      Alive = false;
      IsDying = false;
      ProcessMode = ProcessModeEnum.Disabled;
    }

    public virtual void MakeAlive()
    {
      Visible = true;
      ProcessMode = ProcessModeEnum.Always;
      Alive = true;
      Interactable = true;
      QueueDeath = false;
      IsDying = false;
    }

    public override void _PhysicsProcess(double delta)
    {
      if (Alive)
      {
        MoveAndSlide();
      }
    }

    public abstract void TakeDamage(int damage);
  }
}
