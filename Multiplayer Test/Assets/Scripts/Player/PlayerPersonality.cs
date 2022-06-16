using UnityEngine;
using Mirror;

public class PlayerPersonality : NetworkBehaviour
{
    [SerializeField] PlayerPersonalityTab _firstTab;
    [SerializeField] PlayerPersonalityTab _secondTab;

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
}
