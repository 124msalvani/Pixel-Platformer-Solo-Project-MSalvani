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
    /* 
   * Name: _Ready
   * Description: On initialization, hides loadingscreen, connects animation to OnFadingFinish
   * Parameters: None
   * Returns: None
   */
    public override void _Ready()
	{
        this.Visible = false;
        screenAnimator.AnimationFinished += OnFadingFinish;
    }

    /* 
  * Name: FadeIn
  * Description: Makes loading screen visible, plays fade in animation
  * Parameters: None
  * Returns: None
  */
    public void FadeIn()
    {
        this.Visible = true;
        screenAnimator.Play("FadeIn");
    }
    /* 
* Name: FadeOut
* Description: Makes loading screen visible, plays fade in animation
* Parameters: None
* Returns: None
*/
    public void FadeOut()
    {
        screenAnimator.Play("FadeOut");

    }
    /* 
* Name: OnFadingFinish
* Description: Handles completion of fading in and out animations, hiding loading screen when FadeOut finishes
* Parameters: StringName finishedAnimation, name of animation that finished
* Returns: None
*/
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
