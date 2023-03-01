using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.NormalJump)]

public class PlayerNormalJumpState : PlayerJumpState
{
    public PlayerNormalJumpState(IFSMEntity owner) : base(owner)
    {
    }
    
    protected override void OnJumpFinish()
    {
        ownerEntity.ChangeState(Player.States.NormalMove);
    }
}
