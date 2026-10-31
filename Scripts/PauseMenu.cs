/********************************************************************************************/
/*  File: PauseMenu.cs
 *  Author: Prof. Jacklitsch
/*  Creation Date:   10/29/26
/*  Last Modified:   10/30/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/

using Godot;
using System;


public partial class PauseMenu : Control
{
	[Export]
	private HSlider _volumeSlider;
    [Export]
    private Button _pauseButton;
    [Export]
    private Button _quitButton;

    // Called when the node enters the scene tree for the first time.
    /* 
    * Name: _Ready
    * Description: On initalization, connects pause menu buttons, initalizes volume slider and connects to UpdateVolume
    * Parameters: None
    * Returns: None
    */
    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Pausable;

        _pauseButton.ButtonUp += TogglePause;

        _volumeSlider.Value = GlobalVariables.GameVolume;
        UpdateVolume(GlobalVariables.GameVolume);

        _volumeSlider.ValueChanged += UpdateVolume;

        _quitButton.Pressed += ExitGame;

    }
    /* 
    * Name: _Input
    * Description: Checks for Pause input, calls TogglePause when detected
    * Parameters: InputEvent @event, input event (ex. Key being pressed)
    * Returns: None
    */
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            TogglePause();
        }
    }

    /* 
    * Name: TogglePause
    * Description: Toggles the game's paused state, makes pause menu visible/not visible
    * Parameters: None
    * Returns: None
    */
    public void TogglePause()
    {
        if (GetTree().Paused == false)
        {
            this.Visible = true;
            ProcessMode = ProcessModeEnum.WhenPaused;
            GetTree().Paused = true;
        }
        else
        {
            this.Visible = false;
            ProcessMode = ProcessModeEnum.Pausable;
            GetTree().Paused = false;
        }
    }

    /* 
    * Name: UpdateVolume
    * Description: Ipdates global volume using new volume, applies audio to master bus
    * Parameters: double newVolume, new volume selected
    * Returns: None
    */
    //# Shout out to Google AI for stating that the paramter must be a double, not a float,
    //#   contrary to Godot's engine's tips (likely a GDScript/C# difference)
    public void UpdateVolume(double newVolume)
    {
        GlobalVariables.GameVolume = (float)newVolume;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), GlobalVariables.GameVolume);
    }

    /* 
    * Name: ExitGame
    * Description: When called, closes the game
    * Parameters: None
    * Returns: None
    */
    public void ExitGame()
    {
        GetTree().Quit();
    }
}
