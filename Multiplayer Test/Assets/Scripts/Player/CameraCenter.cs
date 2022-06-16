using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = _playerTransform.position;
        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x, 
                                                _playerTransform.rotation.eulerAngles.y, 
                                                _transform.rotation.eulerAngles.z);
    }
}
