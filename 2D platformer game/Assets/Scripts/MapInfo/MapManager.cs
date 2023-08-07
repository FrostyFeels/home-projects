using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Manager
{
    public MapGenerator Gen;
    public MapDrawer Drawer;
    public MapTileUpdater Updater;
    public MapStateMachine MapState;
    public MapShowCaser Showcaser;
    public MapSave Save;
    public MapLoad Load;
    public Map map;
    public TileSelector Selector;
    public BuildStateMachine BuildState;
}
