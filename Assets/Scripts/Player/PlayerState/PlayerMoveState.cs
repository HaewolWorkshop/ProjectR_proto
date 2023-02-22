using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveState : FSMState<Player>
{
    private readonly int ForwardAnimParam = Animator.StringToHash("Forward");

    private float moveSpeed = 3; //임시 속도

    private Vector2 moveInput;
    private Vector2 lookInput;

    public PlayerMoveState(IFSMEntity owner) : base(owner)
    {
    }


    public override void InitializeState()
    {
        ownerEntity.onMove = (x) => moveInput = x;
        ownerEntity.onLook = (x) => lookInput = x;
    }

    public override void UpdateState()
    {
        ownerEntity.HandleCameraRotation(lookInput, moveInput != Vector2.zero);
    }

    public override void FixedUpdateState()
    {
        var realSpeed = ownerEntity.rigidbody.velocity.magnitude;
        ownerEntity.animator.SetFloat(ForwardAnimParam, realSpeed / moveSpeed);

        var velocity = ownerEntity.ConvertToCameraSpace(new Vector3(moveInput.x, 0, moveInput.y)) * moveSpeed;
        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
    }


    public override void ClearState()
    {
    }
}