using Godot;

using Dynamics;

namespace Players
{
	public partial class BulletHitbox : Hitbox
	{
		[Export]
		public int Damage = 10;
	}
}
