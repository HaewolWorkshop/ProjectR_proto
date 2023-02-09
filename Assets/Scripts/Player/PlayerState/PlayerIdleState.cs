using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : FSMState<Player>
{
    private Transform mainCameraTransform;

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
    }

    public override void ClearState()
    {
    }

    private void OnMove(Vector2 input)
    {
        Debug.Log(input);

        var dir = mainCameraTransform.forward * input.y;
        dir += mainCameraTransform.right * input.x;
        //dir.Normalize();

        dir *= 10; // 임시 속도

        //var projectedVelocity = Vector3.ProjectOnPlane(dir, )

        ownerEntity.rigidbody.velocity = dir;

        HandleRotation(input);
    }


    private void HandleRotation(Vector2 input)
    {
        var dir = Vector3.zero;

        dir = mainCameraTransform.forward * input.y;
        dir += mainCameraTransform.right * input.x;

        dir.Normalize();
        dir.y = 0;

        if (dir == Vector3.zero)
        {
            dir = ownerEntity.transform.forward;
        }

        var look = Quaternion.LookRotation(dir);
        var targetRotation = Quaternion.Slerp(ownerEntity.transform.rotation, look, 10 * Time.deltaTime); // 임시 속도 2

        ownerEntity.transform.rotation = targetRotation;
    }


}