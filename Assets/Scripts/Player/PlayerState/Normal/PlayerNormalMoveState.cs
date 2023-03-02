using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.NormalMove)]
public class PlayerNormalMoveState : PlayerMoveState
{
    protected override PlayerData data => ownerEntity.Data[0];

    public PlayerNormalMoveState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        base.InitializeState();

        ownerEntity.SetAction(Player.ButtonActions.Jump, OnJump);
        ownerEntity.SetAction(Player.ButtonActions.Stealth, OnStealth);
        ownerEntity.SetAction(Player.ButtonActions.Sprint, OnSprint);
        ownerEntity.SetAction(Player.ButtonActions.Henshin, OnHenshin);
    }

    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Jump);
        ownerEntity.ClearAction(Player.ButtonActions.Stealth);
        ownerEntity.ClearAction(Player.ButtonActions.Sprint);
        ownerEntity.ClearAction(Player.ButtonActions.Henshin);
    }

    private void OnJump(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.NormalJump);
        }
    }

    private void OnStealth(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.NormalStealth);
        }
    }

    private void OnSprint(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.NormalSprint);
        }
    }

    private void OnHenshin(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.Henshin);
        }
    }
}