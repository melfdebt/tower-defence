using Godot;
using System;
using System.Formats.Tar;
using System.Runtime.InteropServices;

[GlobalClass]
public partial class MoveComponent : Node
{
	protected CharacterBody3D parentNode;
	[Export]
	protected float Speed =0.5f;
	protected Node3D Target;

	[Export]
	protected float StopRadius = 0.5f;


	public override void _Ready()
	{
		if (GetParent() is CharacterBody3D p)
		{
			parentNode = p;
		}
	}

	public void SetTarget(Node3D target)
	{
		Target = target;
	}

    public override void _PhysicsProcess(double delta)
    {
        if (parentNode != null)
		{
			Move(parentNode);
		}
    }

	private void Move(CharacterBody3D p)
	{
		/*Vector3 pos;

		if (Target == null)
		{
			pos = Vector3.Zero;
		}
		else
		{
			pos = Target.GlobalPosition;
		}
		Vector3 dir = pos-p.GlobalPosition;
		dir=dir.Normalized();
		p.Velocity = dir*Speed;
		p.MoveAndSlide();*/
		Vector3 diff;
		if (Target == null)
		{
			diff=(Vector3.Zero - p.GlobalPosition);
		}
		else
		{
			diff = Target.GlobalPosition - p.GlobalPosition;
		}
		Vector3 direction =  diff.Normalized();
		if (diff.Length() > StopRadius)
		{
			Vector3 velocity  = new Vector3(
				direction.X*Speed,
				0,
				direction.Z*Speed
			);
			p.Velocity = velocity;
			p.MoveAndSlide();
		}
		else
		{
			p.Velocity = Vector3.Zero;
			p.MoveAndSlide();
		}
	}


}
