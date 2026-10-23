using Godot;
using System;
using System.Threading.Tasks;

public partial class CharacterController : CharacterBody2D
{
    [Signal]
    public delegate void EnteredZoneEventHandler(Area2D cameraZone);

    [Signal]
    public delegate void StartedJumpEventHandler();

    [Export]
    private PlayerAnimationController _animationHandler;

    public const float PixelsPerMeter = 16;
    public const float MHorizonalSpeed = 10f;
	public const float JumpVelocity = -25f;

    public override void _Ready()
    {

    }


    public override void _PhysicsProcess(double delta)
	{
		Vector2 previousVelocity = this.Velocity;
        Vector2 currentVelocity = new Vector2(0, previousVelocity.Y);

        // Movement
        float horizontalDirection = 0;
        if (Input.IsActionPressed("MoveRight"))
        {
            horizontalDirection += 1;
        }
        if (Input.IsActionPressed("MoveLeft"))
        {
            horizontalDirection -= 1;
        }
        currentVelocity.X = horizontalDirection * PixelsPerMeter * MHorizonalSpeed;

        // Add the gravity.
        if (!IsOnFloor())
		{
            // Custom Arc
            if (currentVelocity.Y < -1 * PixelsPerMeter)
            {
                currentVelocity += GetGravity() * (float)delta;
            }
            else if (currentVelocity.Y <= 1 * PixelsPerMeter)
            {
                currentVelocity += GetGravity() * (float)delta * 0.25f;
            }
            else
            {
                currentVelocity += GetGravity() * (float)delta * 2f;
            }
		}
        // Handle Jump
        if (Input.IsActionJustPressed("Jump") && IsOnFloor())
        {
            currentVelocity.Y = JumpVelocity * PixelsPerMeter;
            EmitSignal(SignalName.StartedJump);
        }


		Velocity = currentVelocity;
		MoveAndSlide();
	}

    
}
