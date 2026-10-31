/********************************************************************************************/
/*  File: StartScreen.cs
/*  Author:  Maggie Salvani
/*  Major:   Game Development
/*  Creation Date:   8/31/26
/*  Last Modified:   9/4/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Assignment 1
/*  Purpose:		Demonstrate your understanding of Godot by creating a simple shooting gallery game.
/********************************************************************************************/

using Godot;
using System;

public partial class MainMenu : Control
{
    [Export] private Control _mainMenuButtons;
    [Export] private Control _settingsMenu;
    [Export] private Control _creditsMenu;
    [Export] private Control _controlsMenu;


    [Export]
	private HSlider _volumeSlider;
    [Export]
    private Button _startButton;
    [Export]
    private Button _quitButton;


    [Export]
    private Button _settingsButton;
    [Export]
    private Button _creditsButton;
    [Export]
    private Button _controlsButton;


    [Export]
    private Button _settingsReturnButton;
    [Export]
    private Button _creditsReturnButton;
    [Export]
    private Button _controlsReturnButton;

    /* 
    * Name: _Ready
    * Description: On initialization, connects start button to the SwitchToNextScene function
    * Parameters: None
    * Returns: None
    */
    public override void _Ready()
    {
        //Main Menu
        _startButton.Pressed += SwitchToNextScene;
        _quitButton.Pressed += ExitGame;
        _settingsMenu.Visible = false;


        //Settings
        _settingsButton.Pressed += SettingsMenu;
        _volumeSlider.Value = GlobalVariables.GameVolume;
        UpdateVolume(GlobalVariables.GameVolume);
        _volumeSlider.ValueChanged += UpdateVolume;
        _settingsReturnButton.Pressed += SettingsReturn;

        //Credits
        _creditsButton.Pressed += CreditsMenu;
        _creditsReturnButton.Pressed += CreditsReturn;

        //Controls
        _controlsButton.Pressed += ControlsMenu;
        _controlsReturnButton.Pressed += ControlsReturn;
    }
    /* 
    * Name: SwitchToNextScene
    * Description: Changes the scene to the main scene
    * Parameters: None
    * Returns: None
    */
    public void SwitchToNextScene()
    {
        //Google Godot C# How to switch scenes
        // Automatically frees the current scene and loads the new one
        GetTree().ChangeSceneToFile("res://Scenes/MainGame.tscn");

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

    public void SettingsMenu()
    {
        _mainMenuButtons.Visible = false;
        _settingsMenu.Visible = true;
    }

    public void SettingsReturn()
    {
        _mainMenuButtons.Visible = true;
        _settingsMenu.Visible = false;
    }

    public void CreditsMenu()
    {
        _mainMenuButtons.Visible = false;
        _creditsMenu.Visible = true;
    }

    public void CreditsReturn()
    {
        _mainMenuButtons.Visible = true;
        _creditsMenu.Visible = false;
    }

    public void ControlsMenu()
    {
        _mainMenuButtons.Visible = false;
        _controlsMenu.Visible = true;
    }

    public void ControlsReturn()
    {
        _mainMenuButtons.Visible = true;
        _controlsMenu.Visible = false;
    }



    public void UpdateVolume(double newVolume)
    {
        GlobalVariables.GameVolume = (float)newVolume;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), GlobalVariables.GameVolume);
    }

}
