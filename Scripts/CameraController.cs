using Godot;
using System;

public partial class CameraController : Camera2D
{
	[Export]
	private CharacterController _trackedTarget;

    [Export]
    private GameManager _gameManager;

    [Export]
    private Area2D _cameraZone;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        ProcessMode = ProcessModeEnum.Always;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Vector2 lastPosition = this.Position;
		Vector2 desiredPosition = _trackedTarget.Position;

        this.Position = desiredPosition;

    }

	public void SwitchZone(Area2D ncameraZone)
    {
        this.LimitLeft = (int)(ncameraZone.Position.X - ncameraZone.GetChild<CollisionShape2D>(0).GetShape().GetRect().Size.X / 2);
        this.LimitRight = (int)(ncameraZone.Position.X + ncameraZone.GetChild<CollisionShape2D>(0).GetShape().GetRect().Size.X / 2);
        this.LimitTop = (int)(ncameraZone.Position.Y - ncameraZone.GetChild<CollisionShape2D>(0).GetShape().GetRect().Size.Y / 2);
        this.LimitBottom = (int)(ncameraZone.Position.Y + ncameraZone.GetChild<CollisionShape2D>(0).GetShape().GetRect().Size.Y / 2);

    }
}