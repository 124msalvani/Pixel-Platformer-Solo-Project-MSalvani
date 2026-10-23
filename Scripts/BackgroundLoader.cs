using Godot;
using System;
using System.Collections.Specialized;

public partial class BackgroundLoader : Node
{
    //! May need to change the parameter to BackgroundLoader if adding additional features (like AdoptiveParent)
    [Signal]
    public delegate void FinishedLoadingEventHandler(Resource loadedResource);

    private string _scenePath;

    //private Resource _loadedResource;
    
    
    //! Maybe need this in future? For now, loaders have no ambiguity concerns
    //public Node AdoptiveParent { get; private set; }


    public void InitializeObject(string scenePath)
    {
        _scenePath = scenePath;
        //AdoptiveParent = adoptiveParent;
        ResourceLoader.LoadThreadedRequest(scenePath, useSubThreads: false);
    }

    public override void _Process(double delta)
    {
        //! _scenePath *shouldn't* be null because InitializeObject is called right after the new()
        ResourceLoader.ThreadLoadStatus loadStatus = ResourceLoader.LoadThreadedGetStatus(_scenePath);
        if (loadStatus == ResourceLoader.ThreadLoadStatus.Loaded)
        {
            EmitSignal(SignalName.FinishedLoading, ResourceLoader.LoadThreadedGet(_scenePath));
            QueueFree();
        }
    }
}
