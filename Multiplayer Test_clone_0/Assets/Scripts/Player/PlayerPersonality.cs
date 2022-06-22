using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerPersonality : NetworkBehaviour
{
    [SerializeField] PlayerPersonalityTab _firstTab;
    [SerializeField] PlayerPersonalityTab _secondTab;
    [SerializeField] Text _firstPointsText;
    [SerializeField] Text _secondPointsText;

    [SyncVar(hook = nameof(InitializeTabs))]
    private string _name;

    public string Name => _name;

    [Command(requiresAuthority = false)]
    public void Initialize(string name)
    {
        _name = name;
    }

    private void InitializeTabs(string oldName, string newName)
    {
        _firstTab.Initialize(newName);
        _secondTab.Initialize(newName);
    }

    public void UpdatePointsVisualization(int count)
    {
        if (isServer)
            RpcUpdatePoints(count);
        else
            CmdUpdatePoints(count);
    }

    [Command]
    private void CmdUpdatePoints(int count)
    {
        RpcUpdatePoints(count);
    }

    [ClientRpc]
    private void RpcUpdatePoints(int count)
    {
        _firstPointsText.text = count.ToString();
        _secondPointsText.text = count.ToString();
    }
}
