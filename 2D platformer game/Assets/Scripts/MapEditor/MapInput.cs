using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInput : MonoBehaviour
{
    [SerializeField] private MapManager _manager;
    [SerializeField] private CharacterManager _eManager;

    public void Update()
    {
        if(_manager.Drawer.enabled)
        {
            UpdateDrawer();
        }

        if(_manager.BuildState.enabled)
        {
            UpdateBuildState();
        }

        if(_eManager.Path.enabled)
        {
            UpdatePath();
        }

        if(_eManager.ESpawn.enabled)
        {
            UpdateSpawn();
        }

        if(_eManager.Position.enabled)
        {
            UpdatePosition();
        }
    }

    public void UpdateDrawer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _manager.Drawer.GetFirstTile();
        }

        if (Input.GetMouseButton(0))
        {
            _manager.Drawer.GetArea();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _manager.Drawer.OnFinish();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _manager.Drawer.UpdateDrawMode(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _manager.Drawer.UpdateDrawMode(false);
        }
    }
    public void UpdateBuildState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _manager.BuildState.OnStateChange(_manager.BuildState.FillingState);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _manager.BuildState.OnStateChange(_manager.BuildState.EmptyState);
        }
    }
    public void UpdateSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _eManager.ESpawn.SpawnEnemy();
        }
    }
    public void UpdatePosition()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _eManager.Position.ChangePosition();
        }
    }
    public void UpdatePath()
    {
        if(Input.GetMouseButton(0))
        {
            _eManager.Path.WritePath();
        }
    }
}
