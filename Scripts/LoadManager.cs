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


    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;
        _loadingScreen.FadedIn += BeginLevelLoad;
        _loadingScreen.FadedOut += CompleteLoadProcess;
        GD.Print("1");
    }


    public void BeginLoadProcess(string scenePath)
    {
        GD.Print("2");
        _scenePath = scenePath;
        GetTree().Paused = true;
        _loadingScreen.FadeIn();

    }

    public void BeginLevelLoad()
    {
        GD.Print("3");
        BackgroundLoader loadHandler = new BackgroundLoader();
        loadHandler.InitializeObject(_scenePath);
        loadHandler.FinishedLoading += CompletedLevelLoad;
        AddChild(loadHandler);
    }

    public void CompletedLevelLoad(Resource loadedLevel)
    {
        GD.Print("4");
        EmitSignal(SignalName.LoadedLevel, loadedLevel);
        _loadingScreen.FadeOut();
    }

    public void CompleteLoadProcess()
    {
        GD.Print("5");
        GetTree().Paused = false;
        EmitSignal(SignalName.FinishedProcess);
    }
}
