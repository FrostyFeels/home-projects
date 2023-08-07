using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : MapState
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
                mapManager.Selector.enabled = true;
                mapManager.Selector.GetSurroundingTiles = true;
                mapManager.Drawer.enabled = true;
                mapManager.BuildState.enabled = true;
                mapManager.Showcaser.EnableEdgeTiles(true);

                foreach (ExpandMap expand in mapManager.map.ExpandMaps)
                {
                    expand.gameObject.SetActive(false);
                }
            }

            if (manager is CharacterManager enemyManager)
            {
                
            }
        }
    }
}
