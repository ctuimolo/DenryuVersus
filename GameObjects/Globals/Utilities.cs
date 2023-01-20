using Godot;

namespace Utilities
{
	public static class Utils
	{
		static Utils()
        {
			GD.Randomize();
		}

		public static float RandomFloat(float from, float to)
        {
			return (float) (from + GD.RandRange(0d, 1d) * (to - from));
        }
	}
}
