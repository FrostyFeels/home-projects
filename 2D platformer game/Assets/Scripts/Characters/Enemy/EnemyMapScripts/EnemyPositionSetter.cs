using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPositionSetter : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TileSelector _selector;
    [SerializeField] private CharacterManager _manager;

    [SerializeField] private Button _resetBtn;
    [SerializeField] private Button _finishBtn;
    [SerializeField] private Button _cancelBtn;

    [SerializeField] private GameObject _confirmObj;
    [SerializeField] private ConfirmButton _confirm;

    private Vector3Int _position;
    private Enemy _enemy;

    private void Start()
    {
        enabled = false;
    }
    public void SetEnemy(Enemy enemy)
    {
        _manager.Stats.gameObject.SetActive(false);
        _enemy = enemy;
        enabled = true;

        _resetBtn.gameObject.SetActive(true);
        _finishBtn.gameObject.SetActive(true);
        _cancelBtn.gameObject.SetActive(true);

        _resetBtn.onClick.AddListener(OnReset);
        _finishBtn.onClick.AddListener(Finish);
        _cancelBtn.onClick.AddListener(OnCancel);
    }

    public void ChangePosition()
    {
        if (_selector.CurrentTile == null) return;

        Vector3Int newPostion = _selector.CurrentTile.GridId + Vector3Int.up;
        if (EnemySpawnManager.SpawnPoints.Contains(newPostion)) return;

        _position = newPostion;
        _enemy.transform.position = newPostion;
    }

    private void OnCancel()
    {
        _confirmObj.SetActive(true);
        _confirm.SetListener(Cancel);
    }

    private void Cancel()
    {
        _resetBtn.onClick.RemoveListener(OnReset);
        _finishBtn.onClick.RemoveListener(Finish);
        _cancelBtn.onClick.RemoveListener(OnCancel);

        _resetBtn.gameObject.SetActive(false);
        _finishBtn.gameObject.SetActive(false);
        _cancelBtn.gameObject.SetActive(false);

        _enemy.transform.position = _enemy.Data.StartPosition;
        _enemy = null;
        enabled = false;
    }

    private void OnReset()
    {
        _enemy.transform.position = _enemy.Data.StartPosition;
    }

    private void Finish()
    {
        _enemy.Data.StartPosition = _position;
        _enemy = null;

        _resetBtn.gameObject.SetActive(false);
        _finishBtn.gameObject.SetActive(false);
        _cancelBtn.gameObject.SetActive(false);

        _resetBtn.onClick.RemoveListener(OnReset);
        _finishBtn.onClick.RemoveListener(Finish);
        _cancelBtn.onClick.RemoveListener(OnCancel);

        enabled = false;
    }
}
