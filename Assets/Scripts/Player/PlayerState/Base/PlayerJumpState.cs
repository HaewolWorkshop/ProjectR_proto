using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerJumpState : FSMState<Player>
{
    private readonly int IsJumpingAnimKey = Animator.StringToHash("IsJumping");

    protected abstract PlayerData data { get; }

    public PlayerJumpState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.animator.SetBool(IsJumpingAnimKey, true);

        var velocity = ownerEntity.rigidbody.velocity;
        velocity.y = data.JumpPower;

        ownerEntity.rigidbody.velocity = velocity;

        ownerEntity.SetAction(Player.ButtonActions.Henshin, OnHenshin);
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        if(ownerEntity.rigidbody.velocity.y <= 0)
        {
            if(ownerEntity.isGrounded)
            {
                ownerEntity.animator.SetBool(IsJumpingAnimKey, false);
                OnJumpFinish();
            }
        }
    }

    public override void ClearState()
    {
        ownerEntity.ClearAction(Player.ButtonActions.Henshin);
    }

    protected abstract void OnJumpFinish();

    private void OnHenshin(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.ChangeState(Player.States.Henshin);
        }
    }
}
