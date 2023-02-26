using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    public enum PlayerStates : int
    {
        Move = 0,
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


    [SerializeField] private PlayerData[] data;
    public PlayerData Data => data[0];

    protected override void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        states = new FSMState<Player>[(int)PlayerStates.Max];

        states[(int)PlayerStates.Move] = new PlayerMoveState(this);

        ChangeState(PlayerStates.Move);
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