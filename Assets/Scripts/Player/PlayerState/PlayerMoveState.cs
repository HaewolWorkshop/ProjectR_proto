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
        HandleCameraRotation(lookInput);

        if (moveInput == Vector2.zero)
        {
            ownerEntity.rigidbody.velocity = Vector3.zero;
            ownerEntity.animator.SetFloat(ForwardAnimParam, 0);
            ownerEntity.ChangeState(Player.PlayerStates.Idle);
        }
    }

    public override void FixedUpdateState()
    {
        var realSpeed = ownerEntity.rigidbody.velocity.magnitude;
        ownerEntity.animator.SetFloat(ForwardAnimParam, realSpeed / moveSpeed);

        //var velocity = (new Vector3(moveInput.x, 0, moveInput.y)).ConvertToTransformSpace(Camera.main.transform) * moveSpeed;
        var velocity = (new Vector3(moveInput.x, 0, moveInput.y)).ConvertToTransformSpace(ownerEntity.transform) * moveSpeed;
        velocity.y = ownerEntity.rigidbody.velocity.y;
        ownerEntity.rigidbody.velocity = velocity;
    }


    public override void ClearState()
    {
        ownerEntity.CameraFollowTarget.localPosition = Vector3.zero;
    }

    public void HandleCameraRotation(Vector2 look)
    {
        var data = ownerEntity.Data;
        var cameraLookAtTarget = ownerEntity.CameraLookAtTarget;
        var cameraFollowTarget = ownerEntity.CameraFollowTarget;
        var ownerTransform = ownerEntity.transform;

        if (data.InverseY)
        {
            look.y *= -1;
        }

        var rotation = cameraLookAtTarget.rotation;

        var target = Quaternion.Euler(0, cameraLookAtTarget.rotation.eulerAngles.y, 0);
        ownerTransform.rotation = Quaternion.Slerp(ownerTransform.rotation, target, data.RotationSpeed * Time.deltaTime);

        cameraLookAtTarget.rotation = rotation * Quaternion.Euler(new Vector3(look.y, look.x, 0) * data.LookSpeed * Time.deltaTime);

        var angles = cameraLookAtTarget.localEulerAngles;
        angles.x = Mathf.Clamp(angles.x > 180 ? angles.x - 360 : angles.x, data.CameraMinYAngle, data.CameraMaxYAngle);
        angles.x = Mathf.MoveTowardsAngle(angles.x, data.CameraCenterAngle, data.CameraReCenterSpeed * Time.deltaTime);
        angles.z = 0;

        cameraLookAtTarget.localEulerAngles = angles;
    }

}