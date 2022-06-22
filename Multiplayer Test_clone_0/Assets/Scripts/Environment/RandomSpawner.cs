using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;

    public Vector3 GetRandomSpawnPoint()
    {
        var randomIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[randomIndex].position;
    }
}
