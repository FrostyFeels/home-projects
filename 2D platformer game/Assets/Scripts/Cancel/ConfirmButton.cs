using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ConfirmButton : MonoBehaviour
{
    [SerializeField] private GameObject _parentUIObject;
    [SerializeField] private Button _button;
    public void Init()
    {
        _button = GetComponent<Button>();
    }

    public void SetListener(UnityAction method)
    {
        gameObject.SetActive(true);
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
        _button.onClick.AddListener(method);
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
