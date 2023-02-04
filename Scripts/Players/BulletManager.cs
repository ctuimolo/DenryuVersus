using Godot;
using System.Collections.Generic;
using World;

namespace Players
{
  public partial class BulletManager : Node2D
  {
    [Export]
    public PackedScene BulletPackage;

    [Export]
    int BulletCount = 128;
    int _bulletIndex = 0;

    [Export]
    public Player Player;

    [Export]
    public AnimationPlayer Animator;

    private List<Bullet> _bullets = new List<Bullet>();

    private WorldManager _worldManager;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");

      for (int i = 0; i < BulletCount; i++)
      {
        Bullet newBullet = BulletPackage.Instantiate<Bullet>();
        newBullet.Manager = this;
        newBullet.Deactivate();
        _bullets.Add(newBullet);
        _worldManager.AddChild(newBullet);
      }

      Animator.Play("idle");
    }

    public void Fire()
    {
      Animator.Play("fire");

      switch(Player.Level)
      {
        case 1:
          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(-7, -6)));
          IncrementBulletIndex();

          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(7, -6)));
          IncrementBulletIndex();
          break;

        case 2:
          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(-4, -6)));
          IncrementBulletIndex();

          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(4, -6)));
          IncrementBulletIndex();

          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(-12, 0)));
          IncrementBulletIndex();

          _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(12, 0)));
          IncrementBulletIndex();
          break;

        default:
          break;
      }

    }

    private void IncrementBulletIndex()
    {
      _bulletIndex++;
      if (_bulletIndex >= _bullets.Count)
      {
        _bulletIndex = 0;
      }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
  }
}