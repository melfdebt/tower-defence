using Godot;
using System;

[GlobalClass]
public partial class VisibilityComponent : Area3D
{


	private Node3D parentNode;

	private Node3D Target;

	[Export]
	public ShootComponent shootComponent;

	[Export]
	public MoveComponent moveComponent;

	private Godot.Collections.Array<Node3D> DetectBodies = new Godot.Collections.Array<Node3D>();


	
	public override void _Ready()
	{
		parentNode = GetParent<Node3D>();
		if (this.GetNodeOrNull<CollisionShape3D>("CollisionShape3D") !=  null){
			this.BodyEntered +=(Node3D body)=>OnBodyEntered(body);
			this.BodyExited+=(Node3D body)=>OnBodyExited(body);
			Godot.Collections.Array<Node3D> overlappingBodies = GetOverlappingBodies();
			for (int i=0; i<overlappingBodies.Count; i++)
			{
				OnBodyEntered(overlappingBodies[i]);
			}
		}

	}

	public void OnBodyEntered(Node3D body)
	{
		bool isInSameGroup = false;
		if ((body.IsInGroup("enemy") && parentNode.IsInGroup("enemy"))
		||(body.IsInGroup("tower") && parentNode.IsInGroup("tower")) )
		{
			isInSameGroup = true;
		}
		if (!isInSameGroup)
		{
			HealthComponent h = body.GetNodeOrNull<HealthComponent>("HealthComponent");
			if (h != null)
			{
				DetectBodies.Add(body);
			}
		}
	}

	public void OnBodyExited(Node3D body)
	{
		DetectBodies.Remove(body);
	}

	public override void _Process(double delta)
	{
		Node3D closestBody=null;
		for (int i = 0; i < DetectBodies.Count; i++)
		{
			Vector3 distancetoTarget = GlobalPosition - DetectBodies[i].GlobalPosition;
			if ((closestBody==null) ||(distancetoTarget.Length() < (GlobalPosition - closestBody.GlobalPosition).Length())){
				closestBody = DetectBodies[i];
			}
		}
		Target = closestBody;
		if (shootComponent != null)
		{
			shootComponent.SetTarget(Target);
		}
		if (moveComponent != null)
		{
			moveComponent.SetTarget(Target);
		}
	}
}
