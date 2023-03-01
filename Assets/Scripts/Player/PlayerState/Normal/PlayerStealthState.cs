using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int)Player.States.NormalStealth)]
public class PlayerStealthState : PlayerMoveState
{
    protected override float moveSpeed => ownerEntity.Data.MoveSpeed;

    public PlayerStealthState(IFSMEntity owner) : base(owner){}

    public override void InitializeState()
    {
        base.InitializeState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        bool keyInput = ownerEntity.GetActionValue(Player.ButtonActions.Stealth);

        if(!keyInput) {
            ownerEntity.animator.SetBool("Stealth", false);
            ownerEntity.ChangeState(Player.States.NormalMove);
        }
    }

    public override void ClearState()
    {

    }
}