using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerJumpState : FSMState<Player>
{
    private readonly int IsJumpingAnimKey = Animator.StringToHash("IsJumping");

    protected Vector2 moveInput;

    protected abstract PlayerData data { get; }

    private float moveSpeed;

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
        ownerEntity.onMove = (x) => moveInput = x;

        moveSpeed = (new Vector2(velocity.x, velocity.z)).magnitude;
    }

    public override void UpdateState()
    {
        var dir = ownerEntity.rigidbody.velocity;
        dir.y = 0;

        if (dir == Vector3.zero)
        {
            return;
        }

        var target = Quaternion.LookRotation(dir, Vector3.up);

        ownerEntity.transform.rotation = Quaternion.Slerp(ownerEntity.transform.rotation, target, data.RotationSpeed * Time.deltaTime);
    }

    public override void FixedUpdateState()
    {
        if(ownerEntity.rigidbody.velocity.y <= 0)
        {
            ownerEntity.rigidbody.AddForce(Vector3.down * data.FallingSpeed, ForceMode.Force);

            if (ownerEntity.isGrounded)
            {
                ownerEntity.animator.SetBool(IsJumpingAnimKey, false);
                OnJumpFinish();
            }
        }

        var velocity = ownerEntity.rigidbody.velocity;

        velocity.y = 0;
        var inputDir = (new Vector3(moveInput.x, 0, moveInput.y)).RotateToTransformSpace(ownerEntity.transform);
        velocity = Vector3.MoveTowards(velocity, inputDir * moveSpeed, data.AirMoveSpeed);

        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
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
