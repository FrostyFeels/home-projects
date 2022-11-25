using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private GameObject _Camera;
    [SerializeField] private GameObject _PlayerPos;
    private Vector3 _WantedPosition, _NormalLocation;
    public bool Shake, returnPos;
    [SerializeField] private float timeSinceShake;
    [SerializeField] private float shakeTime;

    [SerializeField] private float shakeForce;

    public void FixedUpdate()
    {
        if(Shake)
        {
            _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, _WantedPosition, timeSinceShake / shakeTime);
            timeSinceShake += Time.fixedDeltaTime;
        }

        if(timeSinceShake > shakeTime && Shake)
        {
            _NormalLocation = _PlayerPos.transform.position;
            _NormalLocation.z = -10;
            Shake = false;
            returnPos = true;
            timeSinceShake = 0;
        }

        if(returnPos)
        {
            _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, _NormalLocation, timeSinceShake / shakeTime);
            timeSinceShake += Time.fixedDeltaTime;
        }

        if (timeSinceShake > shakeTime && returnPos)
        {
            returnPos = false;
            timeSinceShake = 0;
        }


    }

    public void GetNeededLocation()
    {      
        _WantedPosition = _Camera.transform.position + CalculateForce(shakeForce);
        Shake = true;
    }

    public Vector3 CalculateForce(float force)
    {
        float angle = Cursor.CalculateToCursor(transform.position);

        float xAngle = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float zAngle = Mathf.Sin(angle * Mathf.PI / 180) * force;

        Vector3 forceApplied = new Vector3(xAngle, zAngle, 0);
        return forceApplied;
    }

}
