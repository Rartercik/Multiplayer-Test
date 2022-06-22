using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _distanceToGround;

    private Rigidbody _rigidbody;

    private bool CanJump => Physics.Raycast(_rigidbody.position, Vector3.down, _distanceToGround, _groundLayer);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        var offset = direction.normalized * _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + (_rigidbody.rotation * offset));
    }

    public void Rotate(float yRotation)
    {
        var finalRotationY = _rigidbody.rotation.eulerAngles.y + (yRotation * _rotationSpeed);
        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x,
                                               finalRotationY,
                                               _rigidbody.rotation.eulerAngles.z);
    }

    public void TryJump()
    {
        if(CanJump)
            _rigidbody.AddForce(Vector3.up * _jumpForce);
    }
}
