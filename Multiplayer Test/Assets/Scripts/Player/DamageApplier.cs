using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DamageApplier : MonoBehaviour
{
    [SerializeField] int _takeDamageTime;
    [SerializeField] UnityEvent _onDamageTaken;
    [SerializeField] UnityEvent _onDamageTakenEnd;

    private Action _externalOnDamageTakenEnd;

    public void ApplyDamage(Action externalOnDamageTakenEnd)
    {
        _onDamageTaken.Invoke();
        _externalOnDamageTakenEnd = externalOnDamageTakenEnd;
        StartCoroutine(StayDamagedFor(_takeDamageTime));
    }

    private IEnumerator StayDamagedFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _onDamageTakenEnd.Invoke();
        _externalOnDamageTakenEnd.Invoke();
    }
}