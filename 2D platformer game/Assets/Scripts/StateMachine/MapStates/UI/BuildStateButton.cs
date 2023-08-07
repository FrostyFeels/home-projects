using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildStateButton : MonoBehaviour
{
    [SerializeField] private MapStateMachine _mStateMachine;
    private Button _stateChangeButton;

    private void Start()
    {
        _stateChangeButton = GetComponent<Button>();

        List<Manager> managers = new List<Manager>() { _mStateMachine.MapManager, _mStateMachine.EnemyManager };
        _stateChangeButton.onClick.AddListener(() => _mStateMachine.OnStateChange(_mStateMachine._buildingState, managers));
    }

    private void OnDestroy()
    {
        _stateChangeButton.onClick.RemoveAllListeners();
    }
}
