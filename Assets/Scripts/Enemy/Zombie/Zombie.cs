using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Zombie : FSMPlayer<Zombie>, IFSMEntity
{
    public enum ZombieStates : int
    {
        Idle = 0,
        Chase,
        Attack,
        Hit,
        Dead,
        
        Max
    }

    public float hp = 10f;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;
    public Transform eye;
    public float sightAngle = 30f;
    public float sightRange = 10f;
    public LayerMask sightLayerMask;
    private static int playerLayer = -1;
    public float attackRange = 3f;

    private float sightHalfAngleInCos;
    private float sightRangeSquared;

    public enum WanderType
    {
        CycleByOrder,
        Random,
    }
    
    public ZombieWanderSpots wanderSpots;
    public float wanderSpotRange = 1.0f;
    public WanderType wanderType = WanderType.Random;
    public int currentWanderingTargetSpot = -1;

    public Player player { get; private set; } = null;

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public static readonly int AttackTrigger = Animator.StringToHash("Attack");
    public static readonly int DeathTrigger = Animator.StringToHash("Death");
    public static readonly int HitTrigger = Animator.StringToHash("Hit");

    private void Awake()
    {
        sightHalfAngleInCos = Mathf.Cos(sightAngle * Mathf.Deg2Rad * 0.5f);
        sightRangeSquared = sightRange * sightRange;
        if (playerLayer < 0)
        {
            playerLayer = LayerMask.GetMask("Player");
            Debug.Log(playerLayer);
        }
        
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        player = FindObjectOfType<Player>(); // TODO
    
        SetUp(ZombieStates.Idle);
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

    public Vector3 GetPlayerBodyCenter()
    {
        // TODO 플레이어의 몸체 중심을 어떻게 하지?
        return player.transform.position + Vector3.up * 1f;
    }

    public bool IsPlayerInSight()
    {
        if (!player)
        {
            return false;
        }
        var t = transform;
        var playerPosition = GetPlayerBodyCenter();
        var eyePosition = eye.position;
        var eyeDirection = t.forward;
        var projectedEyeDirection = eyeDirection;
        projectedEyeDirection.y = 0f; projectedEyeDirection.Normalize();

        var toPlayer = playerPosition - eyePosition;
        var toPlayerDirection = toPlayer.normalized;
        var projectedToPlayerDirection = toPlayerDirection;
        projectedToPlayerDirection.y = 0f; projectedToPlayerDirection.Normalize();

        // 시야각 밖에 있으면 무시
        if (Vector3.Dot(projectedEyeDirection, projectedToPlayerDirection) < sightHalfAngleInCos)
        {
            return false;
        }

        // 시야범위 밖에 있으면 무시
        if (playerPosition.DistanceSquared(eyePosition) > sightRangeSquared)
        {
            return false;
        }

        // 물체 검사
        if (!Physics.Raycast(eyePosition, toPlayerDirection, out var hitInfo, sightRange, sightLayerMask.value))
        {
            return false;
        }

        
        // 플레이어가 아닌 것하고 맞았으면 실패 (장애물에 가림)
        if ((1 << hitInfo.collider.gameObject.layer & playerLayer) == 0)
        {
            Debug.DrawLine(eyePosition, hitInfo.point, Color.red);
            return false;
        }
        Debug.DrawLine(eyePosition, hitInfo.point, Color.green);
        
        return true;
    }

    /// <summary>
    /// 애니메이션 이벤트 (Animations/Zombie/Attack)
    /// </summary>
    public UnityAction OnAttack { get; set; }
    public UnityAction<string> OnAnimationEnd { get; set; }
    private void OnAnimationAttackEvent() => OnAttack?.Invoke();
    private void OnAnimationEndEvent(string type) => OnAnimationEnd?.Invoke(type);

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var t = transform;
        var position = t.position;
        // 공격 범위 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, attackRange);

        // 시야각 그리기
        var eyePosition = eye.position;
        var sightHalfAngleInRadian = sightAngle * Mathf.Deg2Rad * 0.5f;
        var forward = t.forward;
        Gizmos.color = Color.cyan;
        var sightLeft = forward.RotatedAroundY(sightHalfAngleInRadian) * sightRange;
        var sightRight = forward.RotatedAroundY(-sightHalfAngleInRadian) * sightRange;
        Gizmos.DrawLine(eyePosition, eyePosition + sightLeft);
        Gizmos.DrawLine(eyePosition, eyePosition + sightRight);

        const int divideCount = 10;
        var sightRotateAngleInRadian = sightHalfAngleInRadian * 2f / divideCount;
        var arcDrawer = sightRight;
        for (int i = 0; i < divideCount; ++i)
        {
            var start = arcDrawer;
            var end = arcDrawer.RotatedAroundY(sightRotateAngleInRadian);
            Gizmos.DrawLine(eyePosition + start, eyePosition + end);
            arcDrawer = end;
        }
    }
#endif

    public bool canDamage = true;
    public void Damage(float amount)
    {
        if (!canDamage)
        {
            return;
        }
        hp -= amount;

        if (hp <= 0)
        {
            hp = 0f;
            ChangeState(ZombieStates.Dead);
        }
        else
        {
            ChangeState(ZombieStates.Hit);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void OnDead()
    {
        Destroy(this.gameObject, 2f);
    }
}