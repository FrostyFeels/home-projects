using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapState : MapState
{
    public override void OnStateDisable(List<Manager> managers)
    {
    }

    public override void OnStateEnable(List<Manager> managers)
    {
        foreach (Manager manager in managers)
        {
            if (manager is MapManager mapManager)
            {
                mapManager.Load.OpenMapOptions();
            }
        }
        
        return;
    }
}
