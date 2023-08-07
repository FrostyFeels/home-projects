using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStateMachine : MonoBehaviour
{
    public MapState _buildingState = new BuildingState();
    public MapState _expandMapState = new ExpendMapState();
    public MapState _characterState = new CharacterPlacementMapState();
    public MapState _loadMapState = new LoadMapState();

    [Header("Expand Map State")]
    public MapManager MapManager;
    public CharacterManager EnemyManager;

    private MapState _currentState;

    private void Start()
    {
        _currentState = _loadMapState;
        List<Manager> manager = new()
        {
            MapManager
        };
        _currentState.OnStateEnable(manager);
    }

    public void OnStateChange(MapState state, List<Manager> managers)
    {
        _currentState.OnStateDisable(managers); 
        _currentState = state;
        _currentState.OnStateEnable(managers);
    }

    public bool CheckState(MapState state)
    {
        return state == _currentState;
    }
}
