using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.HenshinMove)]
public class PlayerHenshinMoveState : PlayerMoveState
{
    protected override PlayerData data => ownerEntity.Data[1];

    public PlayerHenshinMoveState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        base.InitializeState();

        ownerEntity.SetAction(Player.ButtonActions.Jump, OnJump);
        ownerEntity.SetAction(Player.ButtonActions.Henshin, OnHenshin);
        ownerEntity.SetAction(Player.ButtonActions.Attack, OnAttack);
        ownerEntity.SetAction(Player.ButtonActions.Grab, OnGrab);
    }

    private void OnAttack(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.HenshinAttackIdle);
        }
    }

    private void OnGrab(bool isOn)
    {
        if (!isOn)
        {
            return;
        }

        ownerEntity.ChangeState(Player.States.HenshinGrab);

    }

    private void OnJump(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.NormalJump);
        }
    }

    private void OnHenshin(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.Disengage);
        }
    }

    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Jump);
        ownerEntity.ClearAction(Player.ButtonActions.Henshin);
        ownerEntity.ClearAction(Player.ButtonActions.Attack);
        ownerEntity.ClearAction(Player.ButtonActions.Grab);
    }
}