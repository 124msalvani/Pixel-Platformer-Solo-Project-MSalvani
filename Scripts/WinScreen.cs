/********************************************************************************************/
/*  File: WinScreen.cs
/*  Author:  Maggie Salvani
/*  Major:   Game Development
/*  Creation Date:   8/31/26
/*  Last Modified:   9/4/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Assignment 1
/*  Purpose:		Demonstrate your understanding of Godot by creating a simple shooting gallery game.
/********************************************************************************************/
using Godot;
using System;

public partial class WinScreen : Control
{
    [Export]
    private Button _quitButton;

    /* 
   * Name: _Ready
   * Description: On initialization, connects quit button to exit game function
   * Parameters: None
   * Returns: None
   */
    public override void _Ready()
    {
        _quitButton.Pressed += ExitGame;
        //When the button is pressed, sends a signal to exitgame
    }
    /* 
    * Name: ExitGame
    * Description: When called, closes the game
    * Parameters: None
    * Returns: None
    */
    public void ExitGame()
    {
        GetTree().Quit();
    }
}
