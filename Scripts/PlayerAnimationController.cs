using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class PlayerAnimationController : AnimationTree
{
    [Export]
    private CharacterController _playerController;

    public bool xisMoving;

    public float FreefallThresholdVelocity = CharacterController.PixelsPerMeter * 4;
    public float FallingThresholdVelocity = CharacterController.PixelsPerMeter * 10f;

    public float ymovementSpeed;

    public bool isGrounded;

    public bool isJumping;

    public bool isShooting;



    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
	{
        _playerController.StartedJump += StartJump;
        _playerController.ShootMagicBullet += ShootMagic;

    }
    public override void _ExitTree()
    {
        _playerController.StartedJump -= StartJump;
        _playerController.ShootMagicBullet -= ShootMagic;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        xisMoving = _playerController.Velocity.X != 0;
        ymovementSpeed = _playerController.Velocity.Y;

        if (xisMoving)
        {
            this.Set("parameters/Idle/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Run/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/ShootMagic/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Crouch/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Ascend/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Freefall/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Descend/blend_position", _playerController.Velocity.Normalized().X);
            this.Set("parameters/Land/blend_position", _playerController.Velocity.Normalized().X);
        }

        isGrounded = _playerController.IsOnFloor();
        if (isGrounded)
        {
            isJumping = false;
        }

    }

    public void StartJump()
    {
        isJumping = true;
    }

    //#Citation: Google Godot C# wait one second
    public async void ShootMagic()
    {
        isShooting = true;
        await Task.Delay(500);
        isShooting = false;
    }
    
}
