using Godot;
using Particles;
using Utilities;
using World;

namespace Enemies
{
  public partial class EnemyBee : Enemy
  {
    [Export]
    private EnemyHitbox Hitbox;

    [Export]
    private Explosion Explosion;

    [Export]
    private PackedScene BulletPackage;
    private EnemyBullet _bullet;

    [Export]
    private Timer _bulletTimer;
    private float _bulletSpeed = 50;

    private bool _dieAfterAnimation = false;

    private Vector2 _velMaxPos = new Vector2(100, 30);
    private Vector2 _velMaxNeg = new Vector2(10, -500);
    private int _currentHealth;

    private WorldManager _worldManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      base._Ready();
      _bulletTimer.WaitTime = (double)Utils.RandomFloat(2, 15);
      _bulletTimer.Stop();
    }

    public override void MakeAlive()
    {
      base.MakeAlive();

      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");

      Shader.Play("idle");

      Animator.CurrentAnimation = "idle";
      Animator.Seek(GD.RandRange(0, Animator.CurrentAnimationLength), true);
      Animator.SpeedScale = Utils.RandomFloat(0.4f, 0.8f);

      Explosion.Visible = false;
      _currentHealth = Health;
      _dieAfterAnimation = false;

      _bullet = BulletPackage.Instantiate<EnemyBullet>();
      _bullet.PlayerInstance = EnemyInstance.PlayerInstance;
      _worldManager.AddChild(_bullet);

      _bulletTimer.Timeout += new System.Action(FireBullet);
      _bulletTimer.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      base._Process(delta);

      if (_dieAfterAnimation && !Animator.IsPlaying() && !Explosion.Animator.IsPlaying())
      {
        QueueDeath = true;
      }
    }

    public override void TakeDamage(int damage)
    {
      _currentHealth -= damage;
      Shader.Play("hit");

      if (_currentHealth <= 0)
      {
        Alive = false;
        _dieAfterAnimation = true;
        IsDying = true;
        Shader.Play("idle");
        Animator.Play("die");
        Velocity = Vector2.Zero;
        Explosion.Visible = true;
        Explosion.Animator.Play("idle");
      }
    }

    private void FireBullet()
    {
      if (Alive && _bullet.ProcessMode == ProcessModeEnum.Disabled)
      {
        Vector2 direction = Utils.GetNormalVectorBetween(GlobalPosition, EnemyInstance.PlayerInstance.ScenePlayer.GlobalPosition);
        _bullet.Fire(GlobalPosition, direction * _bulletSpeed);
      }
    }
  }
}
