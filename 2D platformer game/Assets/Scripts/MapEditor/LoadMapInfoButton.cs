using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadMapInfoButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetButton(string assetPath, MapLoad mapLoad)
    {
        _button.onClick.AddListener(() => mapLoad.LoadMap(assetPath));
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }



}
