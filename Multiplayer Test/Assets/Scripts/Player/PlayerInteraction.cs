using UnityEngine;
using Zenject;
using Mirror;

[RequireComponent(typeof(RushInstance))]
[RequireComponent(typeof(DamageApplier))]
[RequireComponent(typeof(PlayerPersonality))]
public class PlayerInteraction : NetworkBehaviour
{
    [Inject] GameEndWindow _gameEndWindow;

    private RushInstance _rushInstance;
    private DamageApplier _damageApplier;
    private int _requrePoints;

    [SyncVar]
    private int _points;
    [SyncVar]
    private InteractionState _state = InteractionState.Default;

    public InteractionState State => _state;

    private void Start()
    {
        _rushInstance = GetComponent<RushInstance>();
        _damageApplier = GetComponent<DamageApplier>();
        _requrePoints = _gameEndWindow.RequrePoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerInteraction player))
        {
            if (State == InteractionState.Attack && hasAuthority)
                AttackPlayer(player);
        }
    }

    public void TryRush()
    {
        if (State == InteractionState.Default)
        {
            _rushInstance.TryRush(() => _state = InteractionState.Attack,
                                    () => _state = InteractionState.Default);
        }
    }

    [ClientRpc]
    public void TryApplyDamage(PlayerInteraction attackingPlayer)
    {
        if (State != InteractionState.Default) return;

        attackingPlayer.AddPoint();
        _state = InteractionState.Damaged;
        _damageApplier.ApplyDamage(() => _state = InteractionState.Default);
    }

    [Command]
    public void AttackPlayer(PlayerInteraction player)
    {
        player.TryApplyDamage(this);
    }

    private void AddPoint()
    {
        _points++;
        if(_points >= _requrePoints)
            WinBattle();
    }

    private void WinBattle()
    {
        var playerName = GetComponent<PlayerPersonality>().Name;
        _gameEndWindow.EndGame(playerName);
    }
}
