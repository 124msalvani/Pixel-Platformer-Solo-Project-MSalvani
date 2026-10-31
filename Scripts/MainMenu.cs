/********************************************************************************************/
/*  File: MainMenu.cs
/*  Author:  Maggie Salvani
/*  Major:   Game Development
/*  Creation Date:   8/31/26
/*  Last Modified:   10/30/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/

using Godot;
using System;

public partial class MainMenu : Control
{
    //! Very, very messy and inefficient, need to rework this
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
    * Description: On initialization, connects various buttons between different UI elements on Main Menu
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

    /* 
    * Name: SettingsMenu
    * Description: Makes the Settings Menu UI visible, and the Main Menu UI not visible
    * Parameters: None
    * Returns: None
    */
    public void SettingsMenu()
    {
        _mainMenuButtons.Visible = false;
        _settingsMenu.Visible = true;
    }
    /* 
    * Name: SettingsReturn
    * Description: Makes the Settings Menu UI not visible, and the Main Menu UI visible
    * Parameters: None
    * Returns: None
    */
    public void SettingsReturn()
    {
        _mainMenuButtons.Visible = true;
        _settingsMenu.Visible = false;
    }

    /* 
    * Name: CreditsMenu
    * Description: Makes the Credits Menu UI visible, and the Main Menu UI not visible
    * Parameters: None
    * Returns: None
    */
    public void CreditsMenu()
    {
        _mainMenuButtons.Visible = false;
        _creditsMenu.Visible = true;
    }
    /* 
    * Name: CreditsReturn
    * Description: Makes the Credits Menu UI not visible, and the Main Menu UI visible
    * Parameters: None
    * Returns: None
    */
    public void CreditsReturn()
    {
        _mainMenuButtons.Visible = true;
        _creditsMenu.Visible = false;
    }
    /* 
    * Name: ControlsMenu
    * Description: Makes the Controls Menu UI visible, and the Main Menu UI not visible
    * Parameters: None
    * Returns: None
    */
    public void ControlsMenu()
    {
        _mainMenuButtons.Visible = false;
        _controlsMenu.Visible = true;
    }
    /* 
    * Name: ControlsReturn
    * Description: Makes the Controls Menu UI not visible, and the Main Menu UI visible
    * Parameters: None
    * Returns: None
    */
    public void ControlsReturn()
    {
        _mainMenuButtons.Visible = true;
        _controlsMenu.Visible = false;
    }


    /* 
    * Name: UpdateVolume
    * Description: Updates the volume at the bus using the global gamevolume and sets it to the new volume
    * Parameters: double newVolume, new volume number
    * Returns: None
    */
    public void UpdateVolume(double newVolume)
    {
        GlobalVariables.GameVolume = (float)newVolume;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), GlobalVariables.GameVolume);
    }

}
