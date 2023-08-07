using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SaveMapStateButton : MonoBehaviour
{
    [SerializeField] private MapManager _manager;
    private Button _stateChangeButton;

    private void Start()
    {
        _stateChangeButton = GetComponent<Button>();
        _stateChangeButton.onClick.AddListener(_manager.Save.SaveMap);
    }

    private void OnDestroy()
    {
        _stateChangeButton.onClick.RemoveAllListeners();
    }
}
