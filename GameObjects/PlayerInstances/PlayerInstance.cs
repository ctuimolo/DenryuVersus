using Godot;

using Backgrounds;
using Players;

namespace PlayerInstances
{
	public partial class PlayerInstance : Node
	{
		[Export]
		PlayerNumbers PlayerNumber;

		[Export]
		PlayerColors PlayerColor;

		[Export]
		PackedScene PlayerPackage;

		[Export]
		SpaceBackground Background;

		[Export]
		StaticBody2D NorthWall;

		[Export]
		StaticBody2D EastWall;

		[Export]
		StaticBody2D SouthWall;

		[Export]
		StaticBody2D WestWall;

		public Player ScenePlayer;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			// Setup collision walls
			NorthWall.Position = new Vector2(Background.Size.x/2, 0);
			NorthWall.Scale = new Vector2(Background.Size.x, 1);

			EastWall.Position = new Vector2(Background.Size.x/2, Background.Size.y);
			EastWall.Scale = new Vector2(Background.Size.x, 1);

			SouthWall.Position = new Vector2(0, Background.Size.y/2);
			SouthWall.Scale = new Vector2(1, Background.Size.y);

			WestWall.Position = new Vector2(Background.Size.x, Background.Size.y/2);
			WestWall.Scale = new Vector2(1, Background.Size.y);

			// Instantiate player
			ScenePlayer = PlayerPackage.Instantiate<Player>();
			ScenePlayer.Position = new Vector2(Background.Size.x/2, Background.Size.y - 80);
			ScenePlayer.SetPlayerNumber(PlayerNumber);
			ScenePlayer.SetPlayerColor(PlayerColor);

			AddChild(ScenePlayer);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}