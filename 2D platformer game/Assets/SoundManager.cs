using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask, _selectable;
    [SerializeField] private float _radius;



    public void CreateNoise(float radius, Vector3Int position)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius, _enemyMask);
        foreach (Collider collider in hits)
        {
            collider.gameObject.GetComponent<INoiseHearable>().OnNouseHeared(position);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CreateNoise(_radius, ReceiveSelectedTile());
        }
    }

    public Vector3Int ReceiveSelectedTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _selectable))
        {
            if (hit.collider.TryGetComponent(out TileStats tileStats))
            {
                return tileStats.GridId;
            }
        }

        return Vector3Int.zero;
    }
}
