using Godot;
using System;


[GlobalClass]
public partial class ShootComponent : Node3D
{
	[Export]
	private PackedScene BulletScene;
	[Export]
	private float BulletDamage =10f;
	[Export]
	private float FireRate = 2f;

	private Node3D Target;
	private Timer _shootTimer = new Timer();

	[Export]
	private float BulletSpeed = 500f;



	
	public override void _Ready()
	{
		_shootTimer.WaitTime = 1.0f/FireRate;
		_shootTimer.OneShot=false;
		AddChild(_shootTimer);
		_shootTimer.Timeout+=()=>TryShoot();
	}

	private void TryShoot()
	{
		if (BulletScene != null)
		{
			SpawnBullet();
		}
	}

	private void SpawnBullet()
	{
		Bullet bullet = BulletScene.Instantiate<Bullet>();
		GetTree().CurrentScene.AddChild(bullet);
		bullet.GlobalPosition = GlobalPosition;
		bullet.GlobalRotation = GlobalRotation;
		bullet.SetSpeed(BulletSpeed);
		bullet.SetDamage(BulletDamage);
	}

	private void TurnOnCannon()
	{
		if (_shootTimer.IsStopped())
		{
			_shootTimer.Start();
		}

	}

	private void TurnOffCannon()
	{
		if (!_shootTimer.IsStopped())
		{
			_shootTimer.Stop();
		}
	}

	public void SetTarget(Node3D target)
	{
		Target = target;
	}


	public override void _Process(double delta)
	{
		if (Target != null)
		{
			LookAt(Target.GlobalPosition);
			TurnOnCannon();
		}
		else
		{
			TurnOffCannon();
			Rotation += new Vector3(
				Rotation.X,
				Rotation.Y * (float)delta *0.5f,
				Rotation.Z
			);
		}

	}
}
