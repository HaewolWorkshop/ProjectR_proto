using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    enum PlayerStates : int
    {
        Idle = 0,
        Move,
        Dash,
        Guard,
        Jump,
        Fall,
        Attack,
        Skill,

        Max
    }

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }

    [SerializeField] private Transform cameraFollowTarget;
    [SerializeField] private float cameraMaxYAngle;
    [SerializeField] private float cameraMinYAngle;
    [SerializeField] private float lookSpeed;
    [SerializeField] private bool inverseY;
    [SerializeField] private float rotationSpeed;

    protected override void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        states = new FSMState<Player>[(int)PlayerStates.Max];

        states[(int)PlayerStates.Idle] = new PlayerIdleState(this);

        ChangeState(PlayerStates.Idle);
    }

    protected override void Awake()
    {
        InitInputs();
        Setup();
    }

    protected override void Update()
    {
        UpdateInputs();

        base.Update();
    }
}