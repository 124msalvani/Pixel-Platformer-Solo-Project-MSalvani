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

    [Signal]
    public delegate void EnteredPortalEventHandler(ZonePortal signalingPortal);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        LoadZone.BodyEntered += OnPlayerEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void OnPlayerEntered(Node2D enteringBody)
    {
        GD.Print(LoadZone.GlobalPosition + " " + enteringBody.GlobalPosition);
        EmitSignal(SignalName.EnteredPortal, this);
    }
}
