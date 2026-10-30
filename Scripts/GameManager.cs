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



    //private MapZone[] _loadedZones;

    [Export]
    private string _startingSceneName;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _loadManager.LoadedLevel += SwapActiveLevel;
        _loadManager.FinishedProcess += EndLoadingZone;

        BeginLoadingZone(_startingSceneName);

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

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
