using Godot;

using World;

namespace Players
{
	public enum PlayerColors
    {
		Red,
		Blue,
    }

	public enum PlayerNumbers
	{
		One,
		Two,
	}

	public struct PlayerInputs
    {
		public string Up;
		public string Down;
		public string Left;
		public string Right;
		public string Button1;
		public string Button2;
		public string Button3;
		public string Button4;
		public string ButtonStart;	
    }

	public partial class Player : CharacterBody2D
	{

		public static PlayerInputs PlayerOneInputMap = new PlayerInputs() 
		{
			Up		= "player1_up",
			Down	= "player1_down",
			Left	= "player1_left",
			Right	= "player1_right",
			Button1 = "player1_button1",
			Button2 = "player1_button2",
			Button3 = "player1_button3",
			Button4 = "player1_button4",
			ButtonStart = "player1_start"
		};

		public static PlayerInputs PlayerTwoInputMap = new PlayerInputs()
		{
			Up		= "player2_up",
			Down	= "player2_down",
			Left	= "player2_left",
			Right	= "player2_right",
			Button1 = "player2_button1",
			Button2 = "player2_button2",
			Button3 = "player2_button3",
			Button4 = "player2_button4",
			ButtonStart = "player2_start"
		};

		private PlayerInputs _playerInputMap;

		[Export]
		private float _shipSpeed = 180f;
		private Vector2 _angularVector = new Vector2(0.71f, 0.71f);

		[Export]
		private AnimationPlayer _shipAnimator;
		[Export]
		private AnimationPlayer _thrustAnimator;
		private WorldManager _worldManager;

		[Export]
		Texture2D RedShipSprites;

		[Export]
		Texture2D BlueShipSprites;

		[Export]
		Sprite2D ShipSprite;

		[Export]
		BulletManager Cannons;

		public PlayerColors PlayerColor;

		public Vector2 VelocityChange = Vector2.Zero;

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
					ShipSprite.Texture = RedShipSprites;
					break;
				case PlayerColors.Blue:
					ShipSprite.Texture = BlueShipSprites;
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

			if (Input.IsActionJustPressed(_playerInputMap.Button1))
            {
				Cannons.Fire();
            }

			if (Input.IsActionPressed(_playerInputMap.Right) && !Input.IsActionPressed(_playerInputMap.Left))
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
			else if (Input.IsActionPressed(_playerInputMap.Left) && !Input.IsActionPressed(_playerInputMap.Right))
			{
				VelocityChange += new Vector2(-_shipSpeed, 0);

				if (_shipAnimator.AssignedAnimation == "idle")
				{
					_shipAnimator.Play("turn left");
				}
				else if (_shipAnimator.AssignedAnimation == "turn right" )
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

			if (Input.IsActionPressed(_playerInputMap.Up) && !Input.IsActionPressed(_playerInputMap.Down))
			{
				VelocityChange += new Vector2(0, -_shipSpeed);
			}
			else if (Input.IsActionPressed(_playerInputMap.Down) && !Input.IsActionPressed(_playerInputMap.Up))
			{
				VelocityChange += new Vector2(0, _shipSpeed);
			}

			if (VelocityChange.x != 0 && VelocityChange.y != 0)
            {
				VelocityChange *= _angularVector;
            }

			if (VelocityChange.y > 0)
            {
				_thrustAnimator.Play("down");
            }

			else if (VelocityChange.y < 0)
            {
				_thrustAnimator.Play("up");
            }

			else
            {
				_thrustAnimator.Play("idle");
            }

			Velocity = VelocityChange;
            MoveAndSlide();
        }

        public override void _PhysicsProcess(double delta)
        {
        }

        public override void _Input(InputEvent @event)
		{
			base._Input(@event);
		}
	}
}