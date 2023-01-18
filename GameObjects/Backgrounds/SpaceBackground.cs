using Godot;
using System.Collections.Generic;


namespace Backgrounds
{
	public partial class SpaceBackground : ColorRect
	{
		[Export]
		PackedScene StarPackage;

		[Export]
		int StarCount = 12;

		[Export]
		int SlowStarCount = 12;

		List<Star> _stars	  = new List<Star>();
		List<Star> _slowStars = new List<Star>();


		[Export]
		Vector2 StarSpeed = new Vector2(0f, 0.14f);

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			for (int i = 0; i < StarCount; i++)
			{
				Star newStar = StarPackage.Instantiate<Star>();

				newStar.Position = new Vector2((float)GD.RandRange(0, Size.x), (float)GD.RandRange(0, Size.y));

				_stars.Add(newStar);
				AddChild(newStar);
			}

			for (int i = 0; i < SlowStarCount; i++)
			{
				Star newStar = StarPackage.Instantiate<Star>();

				newStar.Slow = true;
				newStar.Position = new Vector2((float)GD.RandRange(0, Size.x), (float)GD.RandRange(0, Size.y));
				_slowStars.Add(newStar);
				AddChild(newStar);
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			foreach (Star star in _stars)
			{
				star.Position += StarSpeed;
				if (star.Position.y > Size.y)
				{
					star.Position = new Vector2(GD.Randi() % Size.x, 0);
				}
			}

			foreach (Star star in _slowStars)
			{
				star.Position += StarSpeed / 3;
				if (star.Position.y > Size.y)
				{
					star.Position = new Vector2(GD.Randi() % Size.x, 0);
				}
			}
		}
	}
}