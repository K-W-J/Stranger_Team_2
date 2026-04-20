using System;
using System.Collections;
using _01_Work.KHJ.CombatUnit;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    
    public virtual void Play(Transform target)
    {
        ParticleSystem[] particleSystem = GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in particleSystem)
        {
            ps.Play();
        }
        Destroy(gameObject, 2f);
    }
}
