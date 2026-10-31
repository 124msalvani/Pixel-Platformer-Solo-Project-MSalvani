/********************************************************************************************/
/*  File: MagicBullet.cs
/*  Author: Prof. Jacklitsch
/*  Current Author:  Maggie Salvani
/*  Major:   Game Development
/*  Creation Date:   10/27/26
/*  Last Modified:   10/29/26
/*  Section: CPSC320 
/*  Professor Name:  Prof. Jacklitsch
/*  Assignment: Project 1
/*  Purpose:	Demonstrate a solid grasp of principles of 2D platformers and pixel art through both design documents and game.
/********************************************************************************************/


using Godot;
using System;

public partial class MagicBullet : CharacterBody2D
{

    [Export]
    private AudioStream _impactAudio;

    public Vector2 BulletVelocity { get; set; }
    //public, static can only change other static, same across all instances of the class

    public static MagicBullet InitalizeObject(Vector2 spawnPosition, Vector2 initalVelocity)
    {

        MagicBullet newBullet = (MagicBullet)ResourceLoader.Load<PackedScene>("res://Scenes/MagicBullet.tscn").Instantiate();

        newBullet.GlobalPosition = spawnPosition;
        newBullet.TopLevel = true; //Position and movement is not dependant on its parents
        newBullet.BulletVelocity = initalVelocity;
        newBullet.Rotation = initalVelocity.Angle();
        return newBullet;
    }
    public override void _Process(double delta)
    {
      //Put in bullet shooting based on muzzle's position, remove anything with mouse stuff
      //Test with sprite flipping, make sure bullet still goes in same direction
    }

    /* 
    * Name: _PhysicsProcess
    * Description: Checks for collision with the bullet every physics frame, if collision is found then it deletes the bullet
    * Parameters: double deltaTime - time in seconds since prev frame
    * Returns: None
    */
    public override void _PhysicsProcess(double deltaTime)
    {
        KinematicCollision2D collidedObject = this.MoveAndCollide(BulletVelocity * (float)deltaTime);

        if (collidedObject != null)
        {
            GD.Print("COLLISION");

            //#Citation: Started with code from c64cosmin, "How to make destructible terrain using tilesets" https://www.youtube.com/watch?v=sACXaQj3JEQ on 10/27
            //#Citation: Code was being iffy due to being outdated, changed to layers, Google AI: Godot custom tile layers C# on 10/29
            //!!This was the best I could get it working, getting the tile coordinates was being really weird
            if (collidedObject.GetCollider() is TileMapLayer tileMapLayer)
            {
                Vector2 collisionPosition = collidedObject.GetPosition();
                //Move into tile to make sure we collided with it
                Vector2 collisionNormal = collidedObject.GetNormal();
                Vector2 tilePosition = collisionPosition - collisionNormal * 2f;

                //Convert global position into tilemap local position
                Vector2 localPosition = tileMapLayer.ToLocal(tilePosition);

                //Convert local position into tile coordinates
                Vector2I tileCoordinates = tileMapLayer.LocalToMap(localPosition);

                //Get the tile data of the tile at those coordinates
                TileData tileData = tileMapLayer.GetCellTileData(tileCoordinates);

                if (tileData != null)
                {
                    if ((bool)tileData.GetCustomData("destructible"))
                    {
                        tileMapLayer.EraseCell(tileCoordinates);
                  
                        //#Citation: SFX from Prof. Jacklitsch recording Audio and Pausing, on 10/29 
                        OneshotAudio destroySFX = new OneshotAudio();
                        GetTree().CurrentScene.AddChild(destroySFX);
                        destroySFX.InitializeObject(_impactAudio, collisionPosition, 100);

                        GD.Print("TILE DESTROYED!");
                    }
                }
            }

            if (collidedObject.GetCollider() is MagicBullet)
            {
                GD.Print("IS BULLET");
            }
            this.QueueFree();
        }
        
    }


    /* 
    * Name: OnTimerTimeout
    * Description: Once timer is up, deletes bullet
    * Parameters: None
    * Returns: None
    */
    //#Citation: Method and Signal connection from c64cosmin, "How to make destructible terrain using tilesets" https://www.youtube.com/watch?v=sACXaQj3JEQ on 10/27
    private void OnTimerTimeout()
    {
        this.QueueFree();
    }
}
