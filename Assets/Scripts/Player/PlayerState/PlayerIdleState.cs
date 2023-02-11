using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : FSMState<Player>
{
    private readonly int ForwardAnimParam = Animator.StringToHash("Forward");
    private Transform mainCameraTransform;

    private float moveSpeed = 3; //임시 속도
    private float rotationSpeed = 10; //임시 속도 2

    public PlayerIdleState(IFSMEntity owner) : base(owner)
    {
        mainCameraTransform = Camera.main.transform;
    }


    public override void InitializeState()
    {
        ownerEntity.onMove = OnMove;
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
        var realSpeed = ownerEntity.rigidbody.velocity.magnitude;
        ownerEntity.animator.SetFloat(ForwardAnimParam, realSpeed / moveSpeed);
    }

    public override void ClearState()
    {
    }

    private void OnMove(Vector2 input)
    {
        var forward = mainCameraTransform.forward;
        var right = mainCameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        right *= input.x;
        forward *= input.y;

        ownerEntity.rigidbody.velocity = (right + forward) * moveSpeed;

        HandleRotation(ownerEntity.rigidbody.velocity);
    }

    private void HandleRotation(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            dir = ownerEntity.transform.forward;
        }

        var look = Quaternion.LookRotation(dir);
        var targetRotation =
            Quaternion.Slerp(ownerEntity.transform.rotation, look, rotationSpeed * Time.deltaTime); // 임시 속도

        ownerEntity.transform.rotation = targetRotation;
    }
}