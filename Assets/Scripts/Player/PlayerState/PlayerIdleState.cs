using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : FSMState<Player>
{
    private Vector2 moveInput;
    private Vector2 lookInput;

    public PlayerIdleState(IFSMEntity owner) : base(owner)
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

        if (moveInput != Vector2.zero)
        {
            ownerEntity.ChangeState(Player.PlayerStates.Move);
        }
    }

    public override void FixedUpdateState()
    {

    }


    public override void ClearState()
    {
    }


    public void HandleCameraRotation(Vector2 look)
    {
        var data = ownerEntity.Data;
        var cameraLookAtTarget = ownerEntity.CameraLookAtTarget;

        if (data.InverseY)
        {
            look.y *= -1;
        }

        cameraLookAtTarget.rotation *= Quaternion.Euler(new Vector3(look.y, look.x, 0) * data.LookSpeed * Time.deltaTime);

        var angles = cameraLookAtTarget.localEulerAngles;

        angles.x = Mathf.Clamp(angles.x > 180 ? angles.x - 360 : angles.x, data.CameraMinYAngle, data.CameraMaxYAngle);
        angles.z = 0;

        cameraLookAtTarget.localEulerAngles = angles;
    }
}