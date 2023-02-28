using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerMoveState : FSMState<Player>
{
    private readonly int InputXAnimParam = Animator.StringToHash("InputX");
    private readonly int InputYAnimParam = Animator.StringToHash("InputY");

    private Vector2 moveInput;

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
                ownerEntity.Data.RotationSpeed * Time.deltaTime);
        }
    }

    public override void FixedUpdateState()
    {
        var moveSpeed = ownerEntity.Data.MoveSpeed;

        ownerEntity.animator.SetFloat(InputXAnimParam, Mathf.Round(moveInput.x * 100f) / 100f);
        ownerEntity.animator.SetFloat(InputYAnimParam, Mathf.Round(moveInput.y * 100f) / 100f);

        var velocity = (new Vector3(moveInput.x, 0, moveInput.y)).RotateToTransformSpace(ownerEntity.transform) *
                       moveSpeed;
        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
    }


    public override void ClearState()
    {
    }
}