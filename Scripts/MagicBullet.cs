using Godot;
using System;


//SCRIPT BY MAGGIE 10/27, put in comment block later <3
public partial class MagicBullet : CharacterBody2D
{
    //Use for animations later
    //[Export]
    //private BulletAnimationController _bulletanimationhandler;

    //const BULLETSPEED = 300;
    public override void _Process(double delta)
    {
      //Put in bullet shooting based on muzzle's position, remove anything with mouse stuff
      //Test with sprite flipping, make sure bullet still goes in same direction
    }

    //#Citation: Method and Signal connection from https://www.youtube.com/watch?v=sACXaQj3JEQ
    private void OnTimerTimeout()
    {
        QueueFree();
    }
}
