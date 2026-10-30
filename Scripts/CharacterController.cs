using Godot;
using System;
using System.Threading.Tasks;

public partial class CharacterController : CharacterBody2D
{
    [Signal]
    public delegate void EnteredZoneEventHandler(Area2D cameraZone);

    [Signal]
    public delegate void StartedJumpEventHandler();

    //Shooting
    [Signal]
    public delegate void ShootMagicBulletEventHandler();
    private bool _canShoot = true;

    [Export]
    private PlayerAnimationController _animationHandler;

    public const float PixelsPerMeter = 16;
    public const float MHorizonalSpeed = 8f;
	public const float JumpVelocity = -24f;
    public float _bulletSpeed = 30;

    private AnimatedSprite2D _bodySprite;
    public Node2D muzzle;
    private Vector2 _originalMuzzlePosition;

    public override void _Ready()
    {
        _bodySprite = GetNode<AnimatedSprite2D>("BodySprite");
        muzzle = GetNode<Node2D>("Muzzle");
        _originalMuzzlePosition = muzzle.Position;
    }

    public override void _Process(double delta)
    {
        //10/27 Addition: Firing Bullet, will be fixed later
        if (Input.IsActionJustPressed("ShootMagic") && IsOnFloor() &&_canShoot)
        {
            GD.Print("Shoot Magic");
            EmitSignal(SignalName.ShootMagicBullet);
            StartShootCooldown();
        }
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

        //10/29 Addition
        //#Citation: Google "Godot c# how to get the flipH property"
        //Flipping the muzzle's position based on if the sprite has been flipped
        bool isFlipped = _bodySprite.FlipH;
        if (isFlipped)
        {
            muzzle.Position = new Vector2(-_originalMuzzlePosition.X, _originalMuzzlePosition.Y);
        } else
        {
            muzzle.Position = _originalMuzzlePosition;
        }

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

    private async void StartShootCooldown()
    {
        await Task.Delay(300);
        ShootBullet();
        await Task.Delay(300);
        _canShoot = true;
    }

    public void ShootBullet()
    {

        //#Citation: Taken from PlayerController from Assignment1, Originally in Process
        Vector2 spawnLocation = muzzle.GlobalPosition;

        Vector2 direction = Vector2.Right.Rotated(Rotation);

        //Flips based on bodySprite position
        if (_bodySprite.FlipH)
        {
            direction = Vector2.Left.Rotated(Rotation);
        }

        Vector2 bulletVelocity = direction * PixelsPerMeter * _bulletSpeed;

        Node2D newBullet = MagicBullet.InitalizeObject(spawnLocation, bulletVelocity);
        GetTree().CurrentScene.AddChild(newBullet);

        _canShoot = false;
    }

    
}
