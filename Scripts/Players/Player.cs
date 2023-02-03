using Backgrounds;
using Godot;
using World;

namespace Players
{
  public partial class Player : CharacterBody2D
  {
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
    private int _cannonDelay = 10;
    private int _cannonTime = 0;

    private PlayerInputs _playerInputMap;
    private WorldManager _worldManager;

    public PlayerColors PlayerColor;
    public Vector2 VelocityChange = Vector2.Zero;
    public SpaceBackground Background;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      _worldManager = GetTree().Root.GetNode<WorldManager>("WorldManager");
      _shipAnimator.Play("idle");
      _thrustAnimator.Play("idle");
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
      VelocityChange = Vector2.Zero;

      CheckFireInput();

      if (Input.IsActionPressed(_playerInputMap.Right) && !Input.IsActionPressed(_playerInputMap.Left))
      {
        VelocityChange += new Vector2(_shipSpeed, 0);

        if (_shipAnimator.AssignedAnimation == "idle")
        {
          _shipAnimator.Play("turn right");
        } else if (_shipAnimator.AssignedAnimation == "turn left")
        {
          _shipAnimator.PlayBackwards("turn left");
          _shipAnimator.Play("turn right");
        }
      } else if (Input.IsActionPressed(_playerInputMap.Left) && !Input.IsActionPressed(_playerInputMap.Right))
      {
        VelocityChange += new Vector2(-_shipSpeed, 0);

        if (_shipAnimator.AssignedAnimation == "idle")
        {
          _shipAnimator.Play("turn left");
        } else if (_shipAnimator.AssignedAnimation == "turn right")
        {
          _shipAnimator.PlayBackwards("turn right");
          _shipAnimator.Play("turn left");
        }
      } else
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

      if (Input.IsActionPressed(_playerInputMap.Up) && !Input.IsActionPressed(_playerInputMap.Down))
      {
        VelocityChange += new Vector2(0, -_shipSpeed);
      } else if (Input.IsActionPressed(_playerInputMap.Down) && !Input.IsActionPressed(_playerInputMap.Up))
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
      } else if (VelocityChange.Y < 0)
      {
        _thrustAnimator.Play("up");
      } else
      {
        _thrustAnimator.Play("idle");
      }

      Velocity = VelocityChange;
    }

    private void CheckFireInput()
    {
      if (_cannonTime > 0)
      {
        _cannonTime--;
      }

      if (_cannonTime <= 0 && Input.IsActionPressed(_playerInputMap.Button1))
      {
        _cannonTime = _cannonDelay;
        _cannons.Fire();
      }
    }

    public override void _PhysicsProcess(double delta)
    {
      MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
      base._Input(@event);
    }
  }
}