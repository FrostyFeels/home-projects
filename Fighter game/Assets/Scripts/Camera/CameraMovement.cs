using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float startY;

    [SerializeField] private ScreenShake shake;

    public void Start()
    {
        startY = transform.position.z;
    }

    public void Update()
    {
        if (shake.returnPos || shake.Shake)
            return;

        if(target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, startY);
        }
    }

}
