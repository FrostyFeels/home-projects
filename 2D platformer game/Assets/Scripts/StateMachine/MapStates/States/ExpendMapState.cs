using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpendMapState : MapState
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
                mapManager.Selector.EmptySelectedTiles();
                mapManager.Showcaser.EnableEdgeTiles(true);
                mapManager.Selector.enabled = false;
                mapManager.BuildState.enabled = false;

                foreach (ExpandMap expand in mapManager.map.ExpandMaps)
                {
                    expand.gameObject.SetActive(true);
                }
            }
        }




    }
}
