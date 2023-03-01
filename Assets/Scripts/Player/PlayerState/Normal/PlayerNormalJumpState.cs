using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.NormalStates.Jump)]

public class PlayerNormalJumpState : PlayerJumpState
{
    public PlayerNormalJumpState(IFSMEntity owner) : base(owner)
    {
    }

    public override void ClearState()
    {
        base.ClearState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void InitializeState()
    {
        base.InitializeState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
