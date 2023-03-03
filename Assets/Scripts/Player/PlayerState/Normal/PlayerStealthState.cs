using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int)Player.States.NormalStealth)]
public class PlayerStealthState : PlayerMoveState
{
    private readonly int IsStealthAnimKey = Animator.StringToHash("IsStealth");
    protected override PlayerData data => ownerEntity.Data[0];

    public PlayerStealthState(IFSMEntity owner) : base(owner){}

    public override void InitializeState()
    {
        base.InitializeState();

        ownerEntity.animator.SetBool(IsStealthAnimKey, true);

        speedMultiplier = data.StealthMoveMultiplier;

        ownerEntity.SetAction(Player.ButtonActions.Stealth, OnStealth, true);
        ownerEntity.SetAction(Player.ButtonActions.Jump, OnJump);
    }
    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Stealth);
        ownerEntity.ClearAction(Player.ButtonActions.Jump);
    }

    private void OnStealth(bool isOn)
    {
        if(!isOn)
        {
            ownerEntity.animator.SetBool(IsStealthAnimKey, false);
            ownerEntity.ChangeState(Player.States.NormalMove);
        }
    }

    private void OnJump(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.NormalJump);
        }
    }
}