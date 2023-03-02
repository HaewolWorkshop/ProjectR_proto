using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.NormalJump)]

public class PlayerNormalJumpState : PlayerJumpState
{
    protected override PlayerData data => ownerEntity.Data[0];

    public PlayerNormalJumpState(IFSMEntity owner) : base(owner)
    {
    }
    
    protected override void OnJumpFinish()
    {
        ownerEntity.RevertToPreviousState();
    }
}
