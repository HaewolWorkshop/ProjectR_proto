using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    enum PlayerStates : int
    {
        Idle = 0,
        Attack,
        Skill,
        Dash,
        Guard,
        Jump,
        Fall,

        Max
    }

    public Rigidbody rigidbody { get; private set; }


    protected override void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();


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