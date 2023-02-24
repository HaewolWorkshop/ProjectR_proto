using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveState : FSMState<Player>
{
    private readonly int ForwardAnimParam = Animator.StringToHash("Forward");

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
        HandleCameraRotation(lookInput, moveInput != Vector2.zero);
    }

    public override void FixedUpdateState()
    {
        var moveSpeed = ownerEntity.Data.MoveSpeed;

        var realSpeed = ownerEntity.rigidbody.velocity.magnitude;
        ownerEntity.animator.SetFloat(ForwardAnimParam, realSpeed / moveSpeed);

        var velocity = (new Vector3(moveInput.x, 0, moveInput.y)).ConvertToTransformSpace(ownerEntity.transform) * moveSpeed;
        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
    }


    public override void ClearState()
    {
    }

    public void HandleCameraRotation(Vector2 look, bool isMoving)
    {
        var data = ownerEntity.Data;
        var cameraLookAtTarget = ownerEntity.CameraLookAtTarget;
        var cameraFollowTarget = ownerEntity.CameraFollowTarget;
        var ownerTransform = ownerEntity.transform;

        if (data.InverseY)
        {
            look.y *= -1;
        }

        if (isMoving)
        {
            var rotation = cameraLookAtTarget.rotation;

            var target = Quaternion.Euler(0, cameraLookAtTarget.rotation.eulerAngles.y, 0);
            ownerTransform.rotation = Quaternion.Slerp(ownerTransform.rotation, target, data.RotationSpeed * Time.deltaTime);

            cameraLookAtTarget.rotation = rotation;
        }

        cameraLookAtTarget.rotation *= Quaternion.Euler(new Vector3(look.y, look.x, 0) * data.LookSpeed * Time.deltaTime);

        var angles = cameraLookAtTarget.localEulerAngles;
        
        angles.x = Mathf.Clamp(angles.x > 180 ? angles.x - 360 : angles.x, data.CameraMinYAngle, data.CameraMaxYAngle);

        if (isMoving)
        {
            angles.x = Mathf.MoveTowardsAngle(angles.x, data.CameraCenterAngle, data.CameraReCenterSpeed * Time.deltaTime);
        }
        angles.z = 0;

        cameraLookAtTarget.localEulerAngles = angles;
    }

}