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
    public Player OwningPlayer;

    [Export]
    public AnimationPlayer Animator;

    List<Bullet> _bullets = new List<Bullet>();

    private WorldManager _worldManager;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");

      for (int i = 0; i < BulletCount; i++)
      {
        Bullet newBullet = BulletPackage.Instantiate<Bullet>();
        newBullet.Visible = false;
        newBullet.Position = new Vector2(-20, -20);
        newBullet.Manager = this;
        _bullets.Add(newBullet);
      }

      Animator.Play("idle");
    }

    public void Fire()
    {
      Animator.Play("fire");

      _worldManager.AddChild(_bullets[_bulletIndex]);
      _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(-6, -6)));
      IncrementBulletIndex();

      _worldManager.AddChild(_bullets[_bulletIndex]);
      _bullets[_bulletIndex].Fire(ToGlobal(Position + new Vector2(6, -6)));
      IncrementBulletIndex();
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