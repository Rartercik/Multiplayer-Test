using UnityEngine;
using Mirror;

public class MainNetworkManager : NetworkManager
{
    private int _playersCount;

    public PlayerStartInfo CreatePlayerInfo()
    {
        var randomSpawner = FindObjectOfType<RandomSpawner>();
        _playersCount++;
        var location = randomSpawner.GetRandomSpawnPoint();
        var name = string.Format("Player {0}", _playersCount);

        return new PlayerStartInfo(location, name);
    }
}
