using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class RushInstance : MonoBehaviour
{
    [SerializeField] float _rushDistance;
    [SerializeField] float _rushDuration;
    [SerializeField] int _rushReloadTime;
    [SerializeField] UnityEvent _onRushStart;
    [SerializeField] UnityEvent _onRushEnd;

    private Rigidbody _rigidbody;
    private Vector3 _startRushPosition;
    private Vector3 _targetRushPosition;
    private float _rushProgress;
    private bool _canRush = true;
    private bool _isRushing;
    private bool _collidesWithWall;
    private Action _externalOnRushEnd;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_isRushing)
        {
            Rush();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(_isRushing)
                StopRush();
            _collidesWithWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _collidesWithWall = false;
        }
    }

    public void TryRush(Action onRush, Action onRushEnd)
    {
        if (_canRush == false || _collidesWithWall) return;

        onRush.Invoke();
        PrepareRush(onRushEnd);
        _onRushStart.Invoke();

        _canRush = false;
    }

    private void PrepareRush(Action onRushEnd)
    {
        _externalOnRushEnd = onRushEnd;
        _isRushing = true;
        _startRushPosition = _rigidbody.position;
        _targetRushPosition = _startRushPosition + (_rigidbody.rotation * Vector3.forward * _rushDistance);
    }

    private void Rush()
    {
        if (_rushProgress < 1)
        {
            _rushProgress += Time.deltaTime / _rushDuration;
            ProcessRush(_rushProgress);
        }
        else
        {
            StopRush();
        }
    }

    private void ProcessRush(float progress)
    {
        var currentPosition = Vector3.Lerp(_startRushPosition, _targetRushPosition, progress);
        _rigidbody.MovePosition(new Vector3(currentPosition.x, _rigidbody.position.y, currentPosition.z));
    }

    private void StopRush()
    {
        _isRushing = false;
        _externalOnRushEnd.Invoke();
        _onRushEnd.Invoke();
        _rushProgress = 0;
        StartCoroutine(ReloadRush(_rushReloadTime));
    }

    private IEnumerator ReloadRush(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        _canRush = true;
    }
}