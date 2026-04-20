using _01_Work.KHJ.CombatUnit;
using UnityEngine;
using UnityEngine.AI;

public class UnitNavMovement : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private float stopOffset = 0.05f;
    [SerializeField] private float rotationSpeed = 10f;

    private CombatUnit _combatUnit;

    public bool IsArrived => !agent.pathPending
                             && agent.remainingDistance <= agent.stoppingDistance + stopOffset;

    public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;

    public void Initialize(CombatUnit combatUnit)
    {
        _combatUnit = combatUnit;
    }

    private void Update()
    {
        if (agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
        {
            LookAtTarget(agent.steeringTarget, true);
        }
    }

    /// <summary>
    /// 지정한 Target위치로 회전하는 함수
    /// </summary>
    /// <param name="target">Vector3 - 바라볼 위치</param>
    /// <param name="isSmooth">boolean - Lerp 적용여부</param>
    public void LookAtTarget(Vector3 target, bool isSmooth = true)
    {
        Vector3 direction = target - _combatUnit.transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

        if (isSmooth)
        {
            _combatUnit.transform.rotation = Quaternion.Slerp(_combatUnit.transform.rotation, lookRotation,
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            _combatUnit.transform.rotation = lookRotation;
        }
    }

    public void SetStop(bool isStop) => agent.isStopped = isStop;
    public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
    public void SetSpeed(float speed) => agent.speed = speed;
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
}
