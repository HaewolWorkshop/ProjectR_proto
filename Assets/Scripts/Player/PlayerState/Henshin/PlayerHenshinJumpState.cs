using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.HenshinJump)]

public class PlayerHenshinJumpState : PlayerJumpState
{
    protected override PlayerData data => ownerEntity.Data[0];

    public PlayerHenshinJumpState(IFSMEntity owner) : base(owner)
    {
    }
    
    protected override void OnJumpFinish()
    {
        ownerEntity.RevertToPreviousState();
    }
}
