using Godot;
using System;

public partial class Bullet : Area3D
{

	private float Speed = 500f;
	private float Damage = 10f;


	public void SetSpeed(float s){Speed=s;}

	public void SetDamage(float d){Damage=d;}
    public override void _PhysicsProcess(double delta)
    {
        Position+= -Transform.Basis.Z*(float)delta;
    }

	public void OnBulletBodyEntered(Node3D body)
	{
		if (body is CharacterBody3D character)
		{
			HealthComponent h = character.GetNodeOrNull<HealthComponent>("HealthComponent");
			if (h != null)
			{
				h.TakeDamage(Damage);
			}
		}
		QueueFree();

	}

}
