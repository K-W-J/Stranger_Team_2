using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navAgent;

    public void MovementSetting(float moveSpeed)
    {
        _navAgent.speed = moveSpeed;
    }

    public void Move(Vector3 pos)
    {
        _navAgent.SetDestination(pos);
    }

    public void Stop()
    {
        _navAgent.SetDestination(transform.position);
    }
    public bool IsMovementStop()
    {
        return !_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance;
    }
}
