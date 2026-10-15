using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
	private Node3D parentNode;
	[Export]
	protected float MaxHealth=100f;
	protected float CurrentHealth;


	public override void _Ready()
	{
		parentNode  =GetParent<Node3D>();
		CurrentHealth=MaxHealth;
	}

	public void TakeDamage(float dmg)
	{
		CurrentHealth-=dmg;
		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		parentNode.QueueFree();
	}
	
}
