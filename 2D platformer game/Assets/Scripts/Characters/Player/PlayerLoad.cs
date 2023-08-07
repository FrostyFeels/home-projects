using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    [SerializeField] private MapManager _mManager;
    [SerializeField] private CharacterManager _eManager;
    [SerializeField] private Player _playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _mManager.Load.onLoad += LoadPlayers;
    }

    private void LoadPlayers(GameObject obj)
    {
        PlayerSaver saver = obj.GetComponent<PlayerSaver>();

        if (saver == null) return;

        int count = 0;
        foreach (Player.PlayerData data in saver.PlayerDatas)
        {
            count++;
            Player player = Instantiate(_playerPrefab);

            player.Data = data;
            player.transform.position = data.SpawnPosition;

            _eManager.PSpawn._players.Add(count, player);

        }
    }


    private void OnDestroy()
    {
        _mManager.Load.onLoad -= LoadPlayers;
    }
}
