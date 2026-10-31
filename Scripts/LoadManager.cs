/********************************************************************************************/
/*  File: CharacterController.cs
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

public partial class LoadManager : Node
{
    [Signal]
    public delegate void LoadedLevelEventHandler(Resource loadedLevel);

    [Signal]
    public delegate void FinishedProcessEventHandler();

    [Export]
    public LoadingScreen _loadingScreen;

    private string _scenePath;


    /* 
   * Name: _Ready
   * Description: On initialization, connects signals for fading the loading screen in and out
   * Parameters: None
   * Returns: None
   */
    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;
        _loadingScreen.FadedIn += BeginLevelLoad;
        _loadingScreen.FadedOut += CompleteLoadProcess;
    }


    /* 
   * Name: BeginLoadProcess
   * Description: Pauses the level, starts loading the loading screen
   * Parameters: None
   * Returns: None
   */
    public void BeginLoadProcess(string scenePath)
    {
        _scenePath = scenePath;
        GetTree().Paused = true;
        _loadingScreen.FadeIn();

    }

    /* 
   * Name: BeginLevelLoad
   * Description: Starts to load the level
   * Parameters: None
   * Returns: None
   */
    public void BeginLevelLoad()
    {
        BackgroundLoader loadHandler = new BackgroundLoader();
        loadHandler.InitializeObject(_scenePath);
        loadHandler.FinishedLoading += CompletedLevelLoad;
        AddChild(loadHandler);
    }

    /* 
   * Name: CompletedLevelLoad
   * Description: Once the level is finished loading, starts fading out the loading screen
   * Parameters: Resource loadedLevel, the Resource for the new level
   * Returns: None
   */
    public void CompletedLevelLoad(Resource loadedLevel)
    {
        EmitSignal(SignalName.LoadedLevel, loadedLevel);
        _loadingScreen.FadeOut();
    }

    /* 
   * Name: CompleteLoadProcess
   * Description: Unpauses the level
   * Parameters: None
   * Returns: None
   */
    public void CompleteLoadProcess()
    {
        GetTree().Paused = false;
        EmitSignal(SignalName.FinishedProcess);
    }
}
