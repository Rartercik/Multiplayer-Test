using UnityEngine;
using Mirror;

public struct PlayerStartInfo : NetworkMessage
{
    public readonly Vector3 StartPosition;
    public readonly string Name;

    public PlayerStartInfo(Vector3 startPosition, string name)
    {
        StartPosition = startPosition;
        Name = name;
    }
}