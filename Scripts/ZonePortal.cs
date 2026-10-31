/********************************************************************************************/
/*  File: PauseMenu.cs
 *  Author: Prof. Jacklitsch
/*  Creation Date:   10/22/26
/*  Last Modified:   10/30/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/

using Godot;
using System;

public partial class ZonePortal : Node2D
{
    [Export]
    public string ConnectedScene { get; private set; }
    [Export]
    public Area2D LoadZone { get; private set; }
    [Export]
    public Node2D SpawnPoint { get; private set; }

    //Opening win screen at end
    [Export]
    public bool IsFinalPortal { get; private set; } = false;

    [Signal]
    public delegate void EnteredPortalEventHandler(ZonePortal signalingPortal);

    // Called when the node enters the scene tree for the first time.
    /* 
    * Name: _Ready
    * Description: On initalization, connects LoadZone's BodyEntered to OnPlayerEntered to detect player collision
    * Parameters: None
    * Returns: None
    */
    public override void _Ready()
    {
        LoadZone.BodyEntered += OnPlayerEntered;
    }

    /* 
    * Name: OnPlayerEntered
    * Description: Emits signal letting the zone know where the player is
    * Parameters: Node2D enteringBody, body that enters portal's area
    * Returns: None
    */
    public void OnPlayerEntered(Node2D enteringBody)
    {
        GD.Print(LoadZone.GlobalPosition + " " + enteringBody.GlobalPosition);
        EmitSignal(SignalName.EnteredPortal, this);
    }
}
