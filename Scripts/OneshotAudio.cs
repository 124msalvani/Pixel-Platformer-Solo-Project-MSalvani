/********************************************************************************************/
/*  File: OneshotAudio.cs
 *  Author: Prof. Jacklitsch
/*  Creation Date:   10/29/26
/*  Last Modified:   10/30/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/
using Godot;
using System;

public partial class OneshotAudio : AudioStreamPlayer2D
{
    /* 
    * Name: InitializeObject
    * Description: On initalization, connects signals for LoadManager, hides win screen
    * Parameters: AudioStream desiredAudio, the audio stream that will be played
    *             Vector2 audioPosition, the position where the audio will be played
    *             int maxDistance, the maximum distance where you can hear the audio
    * Returns: None
    */
    public void InitializeObject(AudioStream desiredAudio, Vector2 audioPosition, int maxDistance)
    {
        this.Stream = desiredAudio;
        this.MaxDistance = maxDistance;
        this.Position = audioPosition;
        this.Bus = "Master";

        this.Finished += TerminateObject;

        this.Play();
    }

    /* 
    * Name: TerminateObject
    * Description: Once audio is done, deletes audio player
    * Parameters: None
    * Returns: None
    */
    public void TerminateObject()
    {
        this.QueueFree();
    }
}
