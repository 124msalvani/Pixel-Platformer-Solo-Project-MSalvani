/********************************************************************************************/
/*  File: BackgroundLoader.cs
 *  Author: Prof. Jacklitsch
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
using System.Collections.Specialized;

public partial class BackgroundLoader : Node
{
    //! May need to change the parameter to BackgroundLoader if adding additional features (like AdoptiveParent)
    [Signal]
    public delegate void FinishedLoadingEventHandler(Resource loadedResource);

    private string _scenePath;

    /* 
    * Name: InitializeObject
    * Description: Stores scenepath and requests that scene loads through the ResourceLoader
    * Parameters: string scenePath, path to scene that will be loaded
    * Returns: None
    */
    public void InitializeObject(string scenePath)
    {
        _scenePath = scenePath;
        //AdoptiveParent = adoptiveParent;
        ResourceLoader.LoadThreadedRequest(scenePath, useSubThreads: false);
    }

    /* 
    * Name: _Process
    * Description: Runs every frame
    * Parameters: double delta, time since previous frame
    * Returns: None
    */
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
