using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] private MapManager _Mmanager;
    [SerializeField] private CharacterManager _eManager;

    private void Start()
    {
        _Mmanager.Save.onSave += SavePlayers;
    }

    private void SavePlayers(GameObject mapholder)
    {
        PlayerSaver saver = mapholder.AddComponent<PlayerSaver>();
        foreach (Player player in _eManager.PSpawn._players.Values)
        {
            saver.PlayerDatas.Add(player.Data);
        }

    }

    private void OnDestroy()
    {
        _Mmanager.Save.onSave -= SavePlayers;
    }
}
