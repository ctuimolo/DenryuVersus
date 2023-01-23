using Godot;
using System;

namespace Enemies
{
	public partial class EnemyPathFollow : PathFollow2D
	{
		[Export]
		private float Speed;

		[Export]
		public float MaxSpeed = 200;

		[Export]
		public float MinSpeed = 0;

		[Export]
		public float Acceleration = 0f;

		private float _currentSpeed;

		public bool Alive = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Progress = 0;
			ProgressRatio = 0;
			_currentSpeed = Speed;
		}

		public void SetSpeed(float speed)
        {
			_currentSpeed = speed;
			Speed = speed;
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (Alive)
			{
				_currentSpeed += Acceleration;
				_currentSpeed = Mathf.Clamp(_currentSpeed, MinSpeed, MaxSpeed);

				Progress += _currentSpeed;

				if(ProgressRatio >= 1)
                {
					Progress = 0;
                }
			}
        }
	}
}