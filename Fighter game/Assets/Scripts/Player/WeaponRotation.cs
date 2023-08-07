using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private Transform ownPos;
    [SerializeField] private Transform mousePos;

    public void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        float angle = Cursor.CalculateToCursor(ownPos.position);
        var offset = 90f;

        transform.rotation = Quaternion.Euler(Vector3.forward * (angle - offset));
    }


}
