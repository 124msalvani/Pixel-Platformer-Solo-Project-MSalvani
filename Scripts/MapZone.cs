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
    public override void _Ready()
    {
        SceneName = this.SceneFilePath;
        foreach (ZonePortal currentPortal in ZonePortals)
        {
            currentPortal.EnteredPortal += PortalTriggered;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void PortalTriggered(ZonePortal triggeringPortal)
    {
        EmitSignal(SignalName.EnteredPortal, triggeringPortal.ConnectedScene);
    }
}
