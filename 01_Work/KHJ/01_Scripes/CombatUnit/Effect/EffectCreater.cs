using UnityEngine;

public class EffectCreater : MonoBehaviour
{
    [SerializeField] private AttackEffect _effect;
    [SerializeField] private Transform _firePos;

    public void PlayEffect(Transform target)
    {
        AttackEffect bullet = Instantiate(_effect, _firePos.position, Quaternion.identity);
        bullet.Play(target);
    }
}
