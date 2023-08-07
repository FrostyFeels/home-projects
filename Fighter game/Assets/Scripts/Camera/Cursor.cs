using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Vector2 pos;


    void Update()
    {      
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }


    public static float CalculateToCursor(Vector2 ownPos)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (pos - (Vector2)ownPos);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
     
        return angle;
    }
}
