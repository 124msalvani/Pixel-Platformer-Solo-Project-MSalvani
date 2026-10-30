using Godot;
using System;

/*
Royalty Free Music: Bensound.com / royalty - free - music
License code: JSF8HZMBZVA2VE8F
Artist: : Benjamin Tissot
*/

public partial class PauseMenu : Control
{
	[Export]
	private HSlider _volumeSlider;
    [Export]
    private Button _pauseButton;
    [Export]
    private Button _quitButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Pausable;

        _pauseButton.ButtonUp += TogglePause;

        _volumeSlider.Value = GlobalVariables.GameVolume;
        UpdateVolume(GlobalVariables.GameVolume);

        _volumeSlider.ValueChanged += UpdateVolume;

    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            TogglePause();
        }
    }

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

    //# Shout out to Google AI for stating that the paramter must be a double, not a float,
    //#   contrary to Godot's engine's tips (likely a GDScript/C# difference)
    public void UpdateVolume(double newVolume)
    {
        GlobalVariables.GameVolume = (float)newVolume;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), GlobalVariables.GameVolume);
    }
}
