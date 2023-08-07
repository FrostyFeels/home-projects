using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CancelButton : MonoBehaviour
{
    [SerializeField] private GameObject _parentUIObject;
    private Button _button;
    public void Init()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _parentUIObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
