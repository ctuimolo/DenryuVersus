using Backgrounds;
using Dynamics;
using Godot;
using World;

namespace Players
{
  public partial class Player : CharacterBody2D, IDamageable
  {
    #region Exports
    [Export]
    private StateAnimator StateAnimator;

    [Export]
    public bool Alive { get; set; } = false;

    [Export]
    public bool Interactable { get; set; } = false;

    [Export]
    private float _shipSpeed = 160f;
    private Vector2 _angularVector = new Vector2(0.71f, 0.71f);

    [Export]
    private AnimationPlayer _shipAnimator;

    [Export]
    private AnimationPlayer _thrustAnimator;

    [Export]
    private Texture2D _redShipSprites;

    [Export]
    private Texture2D _blueShipSprites;

    [Export]
    private Sprite2D _shipSprite;

    [Export]
    private BulletManager _cannons;

    [Export]
    public int Level = 1;

    [Export]
    private float _cannonDelay = 10;
    private float _cannonTime = 0;

    [Export]
    public Hitbox Hitbox;
    #endregion

    private bool _doFire      = false;
    private bool _doMoveUp    = false;
    private bool _doMoveDown  = false;
    private bool _doMoveLeft  = false;
    private bool _doMoveRight = false;

    private PlayerInputs _playerInputMap;
    private WorldManager _worldManager;

    public PlayerInstance PlayerInstance;
    public PlayerColors PlayerColor;
    public Vector2 VelocityChange = Vector2.Zero;
    public SpaceBackground Background;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      ZIndex = Consts.ZORDER_PLAYER;

      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");
      _shipAnimator.Play("idle");
      _thrustAnimator.Play("idle");

      MakeAlive();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      CheckFireInput();
      CheckMovement();
    }

    public void MakeAlive()
    {
      StateAnimator.ClearQueue();
      StateAnimator.Play("Alive and idle");
    }

    public void Spawn()
    {
      StateAnimator.ClearQueue();
      StateAnimator.Play("respawn invuln");
      StateAnimator.Queue("Alive and idle");
      PlayerInstance.RepositionPlayerToSpawn();
    }

    public void TakeDamage(int damage)
    {
      StateAnimator.ClearQueue();
      StateAnimator.Play("hit");
      PlayerInstance.StateAnimator.Play("flash");
    }

    private void CheckMovement()
    {
      VelocityChange = Vector2.Zero;

      if (Alive)
      {
        if (_doMoveRight && !_doMoveLeft)
        {
          VelocityChange += new Vector2(_shipSpeed, 0);

          if (_shipAnimator.AssignedAnimation == "idle")
          {
            _shipAnimator.Play("turn right");
          }
          else if (_shipAnimator.AssignedAnimation == "turn left")
          {
            _shipAnimator.PlayBackwards("turn left");
            _shipAnimator.Play("turn right");
          }
        }
        else if (_doMoveLeft && !_doMoveRight)
        {
          VelocityChange += new Vector2(-_shipSpeed, 0);

          if (_shipAnimator.AssignedAnimation == "idle")
          {
            _shipAnimator.Play("turn left");
          }
          else if (_shipAnimator.AssignedAnimation == "turn right")
          {
            _shipAnimator.PlayBackwards("turn right");
            _shipAnimator.Play("turn left");
          }
        }
        else
        {
          if (!_shipAnimator.IsPlaying() && _shipAnimator.AssignedAnimation == "turn right")
          {
            _shipAnimator.PlayBackwards("turn right");
            _shipAnimator.Queue("idle");
          }

          if (!_shipAnimator.IsPlaying() && _shipAnimator.AssignedAnimation == "turn left")
          {
            _shipAnimator.PlayBackwards("turn left");
            _shipAnimator.Queue("idle");
          }
        }

        if (_doMoveUp && !_doMoveDown)
        {
          VelocityChange += new Vector2(0, -_shipSpeed);
        }
        else if (_doMoveDown && !_doMoveUp)
        {
          VelocityChange += new Vector2(0, _shipSpeed);
        }

        if (VelocityChange.X != 0 && VelocityChange.Y != 0)
        {
          VelocityChange *= _angularVector;
        }

        if (VelocityChange.Y > 0)
        {
          _thrustAnimator.Play("down");
        }
        else if (VelocityChange.Y < 0)
        {
          _thrustAnimator.Play("up");
        }
        else
        {
          _thrustAnimator.Play("idle");
        }
      }

      Velocity = VelocityChange;
    }

    private void CheckFireInput()
    {
      if (Alive)
      {
        if (_cannonTime > 0)
        {
          switch (Level)
          {
            case 1:
              _cannonTime -= 1;
              break;

            case 2:
              _cannonTime -= 1.2f;
              break;

            default:
              break;
          }
        }

        if (_cannonTime <= 0 && _doFire)
        {
          _cannonTime = _cannonDelay;
          _cannons.Fire();
        }
      }
    }

    public void SetPlayerColor(PlayerColors color)
    {
      switch (color)
      {
        case PlayerColors.Red:
          _shipSprite.Texture = _redShipSprites;
          break;
        case PlayerColors.Blue:
          _shipSprite.Texture = _blueShipSprites;
          break;
      }
    }

    public void SetPlayerNumber(PlayerNumbers number)
    {
      switch (number)
      {
        case PlayerNumbers.One:
          _playerInputMap = PlayerOneInputMap;
          break;
        case PlayerNumbers.Two:
          _playerInputMap = PlayerTwoInputMap;
          break;
      }
    }

    public override void _PhysicsProcess(double delta)
    {
      MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
      base._Input(@event);

      if(@event.Device == PlayerInstance.DeviceNumber)
      {
        if(@event.IsActionPressed(_playerInputMap.Button1))
        {
          _doFire = true;
        }
        if (@event.IsActionReleased(_playerInputMap.Button1))
        {
          _doFire = false;
        }
        if (@event.IsActionPressed(_playerInputMap.Up))
        {
          _doMoveUp = true;
        }
        if (@event.IsActionReleased(_playerInputMap.Up))
        {
          _doMoveUp = false;
        }
        if (@event.IsActionPressed(_playerInputMap.Down))
        {
          _doMoveDown = true;
        }
        if (@event.IsActionReleased(_playerInputMap.Down))
        {
          _doMoveDown = false;
        }
        if (@event.IsActionPressed(_playerInputMap.Left))
        {
          _doMoveLeft = true;
        }
        if (@event.IsActionReleased(_playerInputMap.Left))
        {
          _doMoveLeft = false;
        }
        if (@event.IsActionPressed(_playerInputMap.Right))
        {
          _doMoveRight = true;
        }
        if (@event.IsActionReleased(_playerInputMap.Right))
        {
          _doMoveRight = false;
        }
      }
    }
  }
}
