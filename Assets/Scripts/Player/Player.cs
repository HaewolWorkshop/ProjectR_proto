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


    protected override void Setup()
    {
        states = new FSMState<Player>[(int)PlayerStates.Max];

        states[(int)PlayerStates.Idle] = new PlayerIdleState(this);

        ChangeState(PlayerStates.Idle);
    }

    private void Awake()
    {
        Setup();
    }

    protected override void Update()
    {
        base.Update();
    }
}