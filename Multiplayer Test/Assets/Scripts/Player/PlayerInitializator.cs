using UnityEngine;
using Mirror;

[RequireComponent(typeof(PlayerPersonality))]
public class PlayerInitializator : NetworkBehaviour
{
    [SerializeField] Transform _playerBottom;
    [SerializeField] CameraCenter _cameraCenter;

    [SyncVar(hook = nameof(Initialize))]
    private PlayerStartInfo _startInfo;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        InitializeCamera();

        SetPlayerInfo();
    }

    [Command]
    private void SetPlayerInfo()
    {
        _startInfo = NetworkManager.singleton.GetComponent<MainNetworkManager>().CreatePlayerInfo();
    }

    private void Initialize(PlayerStartInfo oldInfo, PlayerStartInfo newInfo)
    {
        var playerPersonality = GetComponent<PlayerPersonality>();
        var bottomCenterDifference = transform.position - _playerBottom.position;
        transform.position = newInfo.StartPosition + bottomCenterDifference;
        playerPersonality.Initialize(newInfo.Name);
    }

    private void InitializeCamera()
    {
        _cameraCenter.gameObject.SetActive(true);
        _cameraCenter.transform.SetParent(null, true);
    }    

    private void OnDisable()
    {
        if(_cameraCenter != null)
            _cameraCenter.gameObject.SetActive(false);
    }
}
