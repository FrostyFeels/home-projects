using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacementMapState : MapState
{
    public override void OnStateDisable(List<Manager> managers)
    {
        foreach (Manager manager in managers)
        {
            if(manager is MapManager mapManager)
            {
                mapManager.Drawer.UpdateDrawMode(true);
                mapManager.Showcaser.EnableEdgeTiles(true);
            }

            if(manager is CharacterManager characterManager) 
            {
                characterManager.ESpawn.OnStateChange(false);
                characterManager.PSpawn.OnStateChange(false);
                characterManager.PSpawn.enabled = false;
                characterManager.ESpawn.enabled = false;
            }
        }
    }

    public override void OnStateEnable(List<Manager> managers)
    {
        Debug.Log("UwU2");
        foreach (Manager manager in managers)
        {
            if (manager is MapManager mapManager)
            {
                mapManager.Drawer.UpdateDrawMode(false);
                mapManager.Showcaser.EnableEdgeTiles(false);

                mapManager.Selector.enabled = true;
                mapManager.BuildState.enabled = false;
                mapManager.Drawer.enabled = false;
                mapManager.Selector.GetSurroundingTiles = false;
                mapManager.Selector.EmptySelectedTiles();

                foreach (ExpandMap expand in mapManager.map.ExpandMaps)
                {
                    expand.gameObject.SetActive(false);
                }
            }

            if (manager is CharacterManager characterManager)
            {
                Debug.Log("UwU");
                characterManager.ESpawn.OnStateChange(true);
                characterManager.PSpawn.OnStateChange(true);
            }
        }   
    }
}
