using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelationManager : MonoBehaviour
{
    [SerializeField] private CancelButton _cancel;
    [SerializeField] private ConfirmButton _confirm;

    private void Start()
    {
        _cancel.Init();
        _confirm.Init();
        gameObject.SetActive(false);
    }
}
