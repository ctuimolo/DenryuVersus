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
      return (float)GD.RandRange(from, to);
    }

    public static int RandomInt(int from, int to)
    {
      return GD.RandRange(from, to);
    }

    public static Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float time)
    {
      Vector2 q0 = p0.Lerp(p1, time);
      Vector2 q1 = p1.Lerp(p2, time);
      Vector2 r = q0.Lerp(q1, time);
      return r;
    }

    public static Vector2 GetSpriteLiteralSize(Sprite2D sprite)
    {
      return sprite.Texture.GetSize() / new Vector2(sprite.Hframes, sprite.Vframes) * sprite.Scale;
    }
  }
}
