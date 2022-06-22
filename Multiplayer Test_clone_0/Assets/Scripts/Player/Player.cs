using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
public class Player : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInteraction _interaction;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _interaction = GetComponent<PlayerInteraction>();
    }

    public void TryRush()
    {
        _interaction.TryRush();
    }

    public void TryMove(Vector3 direction)
    {
        if(_interaction.State != InteractionState.Attack)
            _movement.Move(direction);
    }

    public void TryRotate(float yRotation)
    {
        if (_interaction.State != InteractionState.Attack)
            _movement.Rotate(yRotation);
    }

    public void TryJump()
    {
        if (_interaction.State != InteractionState.Attack)
            _movement.TryJump();
    }
}
