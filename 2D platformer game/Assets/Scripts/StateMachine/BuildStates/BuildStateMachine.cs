using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStateMachine : MonoBehaviour
{
    public DrawState FillingState = new FillingState();
    public DrawState EmptyState = new EmptyState();
   
    private DrawState _currentState;

    void Start()
    {
        _currentState = FillingState;
    }

    public List<Tile> ReceiveTiles(Vector3Int start, Vector3Int end, bool checkEnabled)
    {
        return _currentState.GetTiles(start, end, checkEnabled);
    }

    public void OnStateChange(DrawState state)
    {
        _currentState = state;
    }
}
