using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
public class PlayerInput : NetworkBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isLocalPlayer == false) return;

        MovePlayer();

        RotatePlayer();

        TryPlayerJump();

        TryPlayerRush();
    }

    private void MovePlayer()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        _player.TryMove(new Vector3(horizontal, 0, vertical));
    }

    private void RotatePlayer()
    {
        var yRotation = Input.GetAxisRaw("Mouse X");

        _player.TryRotate(yRotation);
    }

    private void TryPlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _player.TryJump();
    }

    private void TryPlayerRush()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _player.TryRush();
    }
}
