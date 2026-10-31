/********************************************************************************************/
/*  File: PlayerAnimationController.cs
 *  Author: Prof. Jacklitsch
/*  Current Author:  Maggie Salvani
/*  Major:   Game Development
/*  Creation Date:   10/22/26
/*  Last Modified:   10/30/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/


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
    /* 
    * Name: _EnterTree
    * Description: Connects StartedJump and ShootMagic signals with their animations
    * Parameters: None
    * Returns: None
    */
    public override void _EnterTree()
	{
        _playerController.StartedJump += StartJump;
        _playerController.ShootMagicBullet += ShootMagic;

    }

    /* 
    * Name: _EnterTree
    * Description: Disconnects StartedJump and ShootMagic signals from their animations when the nodes is done in the scene tree
    * Parameters: None
    * Returns: None
    */
    public override void _ExitTree()
    {
        _playerController.StartedJump -= StartJump;
        _playerController.ShootMagicBullet -= ShootMagic;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    /* 
    * Name: _Process
    * Description: Runs every frame, updating player's movement and checking whether the player is grounded to update their jumping
    * Parameters: double delta, time since previous frame
    * Returns: None
    */
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
    /* 
    * Name: StartJump
    * Description: Sets isJumping to true, letting the jump animation play
    * Parameters: None
    * Returns: None
    */
    public void StartJump()
    {
        isJumping = true;
    }

    /* 
    * Name: ShootMagic
    * Description: Sets isShooting to true, triggers animation, and waits before setting it back to false
    * Parameters: None
    * Returns: None
    */
    //#Citation: Taken from Google: "Godot C# wait one second" on 10/29
    public async void ShootMagic()
    {
        isShooting = true;
        await Task.Delay(500);
        isShooting = false;
    }
    
}
