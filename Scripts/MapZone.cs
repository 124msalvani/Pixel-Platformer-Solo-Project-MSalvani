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

public partial class MapZone : Node2D
{
    public string SceneName { get; private set; }

    [Export]
    public Area2D CameraZone { get; private set; }

    [Export]
    public Area2D ZoneBoundary { get; private set; }

    [Export]
    public ZonePortal[] ZonePortals { get; private set; }

    

    [Signal]
    public delegate void EnteredPortalEventHandler(string anticipatedScene);

    // Called when the node enters the scene tree for the first time.
    /* 
    * Name: _Ready
    * Description: On initialization, gets the current scene file's path, and connects each portal
    * Parameters: None
    * Returns: None
    */
    public override void _Ready()
    {
        SceneName = this.SceneFilePath;
        foreach (ZonePortal currentPortal in ZonePortals)
        {
            currentPortal.EnteredPortal += PortalTriggered;
        }
    }

    /* 
    * Name: PortalTriggered
    * Description: When a portal is entered, checks if it is the final portal, emits signal with destination scene
    * Parameters: ZonePortal triggeringPortal, the portal the player entered
    * Returns: None
    */
    public void PortalTriggered(ZonePortal triggeringPortal)
    {
        if (triggeringPortal.IsFinalPortal)
        {
            EmitSignal(SignalName.EnteredPortal, "WIN");
        } else 
            EmitSignal(SignalName.EnteredPortal, triggeringPortal.ConnectedScene);
    }
}
