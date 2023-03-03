using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Zombie : FSMPlayer<Zombie>, IFSMEntity
{
    public enum ZombieStates : int
    {
        Idle = 0,
        Chase,
        Attack,
        Dead,
        
        Max
    }

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;
    public float sightAngle = 30f;
    public float sightRange = 10f;
    public float attackRange = 3f;

    public Player player { get; private set; } = null;

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Death = Animator.StringToHash("Death");

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        player = FindObjectOfType<Player>(); // TODO
    
        SetUp(ZombieStates.Chase);
    }

    private Vector3 moveTarget;
    private bool doMove = false;
    public void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        doMove = true;
    }

    public void StopMove()
    {
        doMove = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var t = transform;
        var position = t.position;
        var targetPosition = moveTarget;

        var toTarget = targetPosition - position;

        var isMoving = toTarget.sqrMagnitude > 0f;
        animator.SetBool(IsMoving, isMoving);
        
        if(!doMove || !isMoving) return;
        var direction = toTarget;
        direction.y = 0;
        direction.Normalize();
        
        rigidbody.MovePosition(position + direction * (moveSpeed * Time.deltaTime));
        rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime));
    }

    /// <summary>
    /// 애니메이션 이벤트 (Animations/Zombie/Attack)
    /// </summary>
    public UnityAction OnAttack { get; set; }
    public UnityAction<string> OnAnimationEnd { get; set; }
    private void OnAnimationAttackEvent() => OnAttack?.Invoke();
    private void OnAnimationEndEvent(string type) => OnAnimationEnd?.Invoke(type);

    private void OnDrawGizmosSelected()
    {
        var t = transform;
        var position = t.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, attackRange);

        var sightHalfAngleInRadian = sightAngle * Mathf.Deg2Rad * 0.5f;
        var forward = t.forward;
        Gizmos.color = Color.cyan;
        var sightLeft = forward.RotatedAroundY(-sightHalfAngleInRadian) * sightRange;
        var sightRight = forward.RotatedAroundY(sightHalfAngleInRadian) * sightRange;
        Gizmos.DrawLine(position, position + sightLeft);
        Gizmos.DrawLine(position, position + sightRight);

    }
}