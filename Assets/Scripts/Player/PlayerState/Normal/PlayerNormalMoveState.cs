using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalMoveState : PlayerMoveState
{
    public PlayerNormalMoveState(IFSMEntity owner) : base(owner)
    {

    }

    public override void InitializeState()
    {
        base.InitializeState();

        // Jump
        ownerEntity.SetAction(Player.ButtonActions.Jump, OnJump);

        // Sitting
        ownerEntity.SetAction(Player.ButtonActions.Stealth, OnStealth);
    }

    private void OnJump(bool isTrigger)
    {
        ownerEntity.ChangeState(Player.PlayerStates.Jump);
    }

    private void OnStealth(bool isTrigger)
    {
        ownerEntity.ChangeState(Player.PlayerStates.Stealth);
    }

    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Jump);
        ownerEntity.ClearAction(Player.ButtonActions.Stealth);
    }
}
