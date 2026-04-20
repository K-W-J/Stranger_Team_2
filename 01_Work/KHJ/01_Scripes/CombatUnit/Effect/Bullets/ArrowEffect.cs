using UnityEngine;
using _01_Work.HS.Core.GameManagement;
using System.Collections;
using _01_Work.HS.Core.Map;

namespace _01_Work.KHJ.CombatUnit
{
    public class ArrowEffect : AttackEffect 
    {
        public float m_InitialAngle = 30f;

        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigid;
        private Transform _target;

        public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
        {
            float gravity = Physics.gravity.magnitude;
            float angle = initialAngle * Mathf.Deg2Rad;

            Vector3 planarTarget = new Vector3(target.x, 0, target.z);
            Vector3 planarPosition = new Vector3(player.x, 0, player.z);

            float distance = Vector3.Distance(planarTarget, planarPosition);
            float yOffset = player.y - target.y;

            float initialVelocity
                = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity
                = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            float angleBetweenObjects
                = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
            Vector3 finalVelocity
                = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            return finalVelocity;
        }
       


        public override void Play(Transform target)
        {
            //_rigid.linearVelocity = (target.position - transform.position).normalized * _speed;
            //transform.LookAt(target.position);
            //StartCoroutine(DestroyCoroutine(Vector3.Distance(target.position, transform.position)));*//*
            float d = Vector3.Distance(target.position, transform.position);
             Vector3 velocity = GetVelocity(transform.position, target.position, 15 + (d - 1) * (60 / 8));
             _rigid.linearVelocity = velocity;

            StartCoroutine(DestroyCoroutine(4));
        }

        private void Update()
        {
            if (_rigid.linearVelocity.sqrMagnitude > 0.01f) 
                transform.rotation = Quaternion.LookRotation(_rigid.linearVelocity);
        }


        private IEnumerator DestroyCoroutine(float distance)
        {
            yield return new WaitForSeconds(distance);
            Destroy(gameObject);
        }
    }
}