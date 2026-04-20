using System;
using System.Collections;
using UnityEngine;

public class TowerAttackEffect : AttackEffect
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigid;

    public override void Play(Transform target)
    {
        base.Play(target);
        transform.LookAt(target.position);
        _rigid.linearVelocity = transform.forward * _speed;
        float t = Vector3.Distance(target.position, transform.position) / _speed;
        StartCoroutine(EffectDestroy(t));
    }

    private IEnumerator EffectDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        AudioManager.Instance.PlaySfx("TOWER_BOMB");
        GetComponentInChildren<EffectCreater>().PlayEffect(null);
        Destroy(gameObject);
    }
}
