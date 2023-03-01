using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerMoveState : FSMState<Player>
{
    private readonly int InputXAnimParam = Animator.StringToHash("InputX");
    private readonly int InputYAnimParam = Animator.StringToHash("InputY");

    private Vector2 moveInput;

    protected abstract PlayerData data { get; }
    protected float speedMultiplier = 1;

    public PlayerMoveState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.onMove = (x) => moveInput = x;
    }

    public override void UpdateState()
    {
        if (moveInput != Vector2.zero)
        {
            var rotation = Camera.main.transform.rotation;
            var ownerTransform = ownerEntity.transform;

            var target = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

            ownerTransform.rotation = Quaternion.Slerp(ownerTransform.rotation, target,
                data.RotationSpeed * Time.deltaTime);
        }
    }

    public override void FixedUpdateState()
    {
        ownerEntity.animator.SetFloat(InputXAnimParam, Mathf.Round(moveInput.x * 100f) / 100f);
        ownerEntity.animator.SetFloat(InputYAnimParam, Mathf.Round(moveInput.y * 100f) / 100f);

        var velocity = (new Vector3(moveInput.x, 0, moveInput.y)).RotateToTransformSpace(ownerEntity.transform) *
                       data.MoveSpeed * speedMultiplier;
        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
    }


    public override void ClearState()
    {
    }
}