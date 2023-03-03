using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.NormalSprint)]
public class PlayerNormalSprintState : PlayerMoveState
{
    private readonly int IsRunningAnimKey = Animator.StringToHash("IsRunning");
    protected override PlayerData data => ownerEntity.Data[0];

    public PlayerNormalSprintState(IFSMEntity owner) : base(owner) { }

    public override void InitializeState()
    {
        ownerEntity.onMove = OnMove;

        ownerEntity.animator.SetBool(IsRunningAnimKey, true);

        speedMultiplier = data.SprintSpeedMultiplier;

        ownerEntity.SetAction(Player.ButtonActions.Sprint, OnSprint, true);
        ownerEntity.SetAction(Player.ButtonActions.Jump, OnJump);
        ownerEntity.SetAction(Player.ButtonActions.Henshin, OnHenshin);
    }

    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Sprint);
        ownerEntity.ClearAction(Player.ButtonActions.Jump);
        ownerEntity.ClearAction(Player.ButtonActions.Henshin);
    }

    private void OnMove(Vector2 input)
    {
        moveInput = input;
        moveInput.x *= data.SprintXAxisMultiplier;
        moveInput.y = Mathf.Clamp(moveInput.y, 0.5f, 1);

        moveInput.Normalize();
    }

    private void OnSprint(bool isOn)
    {
        if (!isOn)
        {
            ownerEntity.animator.SetBool(IsRunningAnimKey, false);
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

    private void OnHenshin(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.Henshin);
        }
    }
}