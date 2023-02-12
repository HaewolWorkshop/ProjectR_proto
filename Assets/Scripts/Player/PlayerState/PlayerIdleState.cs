using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : FSMState<Player>
{
    private readonly int ForwardAnimParam = Animator.StringToHash("Forward");

    private float moveSpeed = 3; //임시 속도
    private float rotationSpeed = 10; //임시 속도 2

    public PlayerIdleState(IFSMEntity owner) : base(owner)
    {
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
        var dir = ownerEntity.ConvertToCameraSpace(new Vector3(input.x, 0, input.y));

        ownerEntity.rigidbody.velocity = dir * moveSpeed;

        if (input != Vector2.zero)
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        var dir = Camera.main.transform.forward;
        dir.y = 0;
        dir.Normalize();

        var look = Quaternion.LookRotation(dir);
        var targetRotation =
            Quaternion.Slerp(ownerEntity.transform.rotation, look, rotationSpeed * Time.deltaTime); // 임시 속도

        ownerEntity.transform.rotation = targetRotation;
    }
}