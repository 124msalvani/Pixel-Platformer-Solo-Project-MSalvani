using Godot;
using System;

public partial class OneshotAudio : AudioStreamPlayer2D
{
    public void InitializeObject(AudioStream desiredAudio, Vector2 audioPosition, int maxDistance)
    {
        this.Stream = desiredAudio;
        this.MaxDistance = maxDistance;
        this.Position = audioPosition;
        this.Bus = "Master";

        this.Finished += TerminateObject;

        this.Play();
    }

    public void TerminateObject()
    {
        this.QueueFree();
    }
}
