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
using System.Linq;
using System.Xml;
using System.Threading.Tasks;

public partial class GameManager : Node2D
{

    [Export]
    private LoadManager _loadManager;

    [Export]
    private Node2D _zonesContainer;

    [Export]
    private CharacterController _playerController;
    
    [Export]
    private CameraController _cameraController;

    [Export]
    private Control _winScreen;

    private MapZone _activeZone;

    private Node2D _spawnPoint;




    [Export]
    private string _startingSceneName;

    // Called when the node enters the scene tree for the first time.
    /* 
    * Name: _Ready
    * Description: On initalization, connects signals for LoadManager, hides win screen
    * Parameters: None
    * Returns: None
    */
    public override void _Ready()
	{
        _loadManager.LoadedLevel += SwapActiveLevel;
        _loadManager.FinishedProcess += EndLoadingZone;

        BeginLoadingZone(_startingSceneName);
        _winScreen.Visible = false;

    }

    /* 
    * Name: SwapActiveLevel
    * Description: Instantiates next level and spawns in player based on portal's connection
    * Parameters: Resource loadedLevel,  scene Resource of the next level
    * Returns: None
    */
    public void SwapActiveLevel(Resource loadedLevel)
    {
        // This is causing collision problems, fixed using async methods
        MapZone nouveauZone = (MapZone)((PackedScene)loadedLevel).Instantiate();
        _zonesContainer.AddChild(nouveauZone);

        if (_activeZone != null)
        {
            string previousScene = _activeZone.SceneFilePath;
            _activeZone.EnteredPortal -= BeginLoadingZone;
            _activeZone.QueueFree();

            _spawnPoint = null;
            foreach (ZonePortal zonePortal in nouveauZone.ZonePortals)
            {
                if (zonePortal.ConnectedScene == previousScene)
                {
                    _spawnPoint = zonePortal.SpawnPoint;
                    break;
                }
            }
            // Debugging
            if (_spawnPoint == null)
            {
                GD.Print("Disaster");
            }
        }
        else
        {
            //! Relies on the premise that at least one zone portal exists
            _spawnPoint = nouveauZone.ZonePortals[0].SpawnPoint;
        }


        _activeZone = nouveauZone;
        _activeZone.EnteredPortal += BeginLoadingZone;
        _cameraController.SwitchZone(_activeZone.CameraZone);
        _playerController.GlobalPosition = _spawnPoint.GlobalPosition;
    }

    /* 
    * Name: BeginLoadingZone
    * Description: Begins loading level when player enters a portal, checks if the portal is the win portal or is one way
    * Parameters: string anticipatedScene, scene that will be loaded
    * Returns: None
    */
    public async void BeginLoadingZone(string anticipatedScene)
    {
        GD.Print("Request Send");

        if (anticipatedScene == "WIN")
        {
            _winScreen.Visible = true;
            _playerController.SetPhysicsProcess(false);
            return;
        }

        // For one-way portals
        if (anticipatedScene == "")
        {
            return;
        }
        // Need to do this becasue of the Instantiate Call's mechanics
        //! If more than just the player risks collision, need to rework
        //! Also, hard-coding collision layer sucks, but oh well
        _playerController.SetCollisionLayerValue(2, false);

        _loadManager.BeginLoadProcess(anticipatedScene);
    }

    /* 
    * Name: EndLoadingZone
    * Description: Waits to make sure the level is loaded before enabling the player's collision
    * Parameters: None
    * Returns: None
    */
    public async void EndLoadingZone()
    {
        // First frame is that which is called before Physics evaluates
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        // Now we wait until the first Physics step is evaluated
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        // Now we can safely re-enable collision
        _playerController.SetCollisionLayerValue(2, true);
    }

    
}


