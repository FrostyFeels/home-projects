using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapState
{
    public abstract void OnStateEnable(List<Manager> managers);
    public abstract void OnStateDisable(List<Manager> managers);
}
