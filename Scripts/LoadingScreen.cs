using Godot;
using System;

public partial class LoadingScreen : CanvasLayer
{
    [Signal]
    public delegate void FadedOutEventHandler();
    [Signal]
    public delegate void FadedInEventHandler();

    private string _loadingScene;

    [Export]
    public AnimationPlayer screenAnimator;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.Visible = false;
        screenAnimator.AnimationFinished += OnFadingFinish;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Loading bar logic could go here
    }

    public void FadeIn()
    {
        this.Visible = true;
        screenAnimator.Play("FadeIn");
    }
    public void FadeOut()
    {
        screenAnimator.Play("FadeOut");

    }
    public void OnFadingFinish(StringName finishedAnimation)
    {
        if (finishedAnimation == "FadeIn")
        {
            EmitSignal(SignalName.FadedIn);
        }
        else if (finishedAnimation == "FadeOut")
        {
            EmitSignal(SignalName.FadedOut);
            this.Visible = false;
        }
    }
}
