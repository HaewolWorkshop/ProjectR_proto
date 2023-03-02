using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Zombie : FSMEnemy<Zombie>, IFSMEntity
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
    public float attackRange = 3f;

    public Player player { get; private set; } = null;

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Death = Animator.StringToHash("Death");

    protected override void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        states = new FSMState<Zombie>[(int)ZombieStates.Max];
        states[(int) ZombieStates.Idle] = new ZombieIdleState(this);
        states[(int) ZombieStates.Chase] = new ZombieChaseState(this);
        states[(int) ZombieStates.Attack] = new ZombieAttackState(this);

        player = FindObjectOfType<Player>(); // TODO
    
        ChangeState(ZombieStates.Chase);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}