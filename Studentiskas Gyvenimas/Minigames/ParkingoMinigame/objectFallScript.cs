using Godot;
using System;
using System.Diagnostics;

public partial class objectFallScript : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public float speed;
	private float screenBottomY;


	Global global;
	CustomSignals customSignals;
	CarInstantiation parentScript;
	public static float fallSpeed;

	public override void _Ready()
	{
		screenBottomY = GetViewportRect().Size.Y;
		parentScript = GetNode<CarInstantiation>("..");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		global = GetNode<Global>("/root/Global");
		customSignals.ParkingMinigameEnded += StopCar;
	}
    public override void _ExitTree()
    {
        customSignals.ParkingMinigameEnded -= StopCar;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		screenBottomY = GetViewportRect().Size.Y + 100;

		var position = Position;
		var deltaY = speed * (float)delta * 50;
		fallSpeed = deltaY;
		Console.WriteLine(fallSpeed);
		position.Y += deltaY;
		Position = position;

		if (Position.Y >= screenBottomY)
			QueueFree();

	}
	void StopCar()
	{
		speed = 0;
	}
	void PointEarned()
	{
	   // Debug.WriteLine("Point earned");
	}
	void OnCarAreaEntered(Area2D area)
	{
		if (area.IsInGroup("player"))
		{
			
			if (Visible == false)
			{
				customSignals.EmitSignal(nameof(CustomSignals.ParkingMinigamePoint));
				global.parkingScore += 1;
				Debug.WriteLine("Point earned");
                Debug.WriteLine($"Parking space pos: {Position}");
                QueueFree();
				//global.CurrentScore();
			}
			else
			{
                Debug.WriteLine($"Parked car pos: {Position}");
                customSignals.EmitSignal(nameof(CustomSignals.ParkingMinigameEnded));
			}


        }

	}
}
