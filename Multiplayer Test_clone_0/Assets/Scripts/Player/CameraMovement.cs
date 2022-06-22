using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _playerCenterTransform;

    private Transform _transform;
    private Vector3 _offset;

    private void Start()
    {
        _transform = transform;
        _offset = _transform.position - _playerCenterTransform.position;
    }

    private void Update()
    {
        _transform.position = _playerCenterTransform.position + _offset;
    }
}
