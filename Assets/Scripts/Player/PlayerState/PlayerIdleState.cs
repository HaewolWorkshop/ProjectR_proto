using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : FSMState<Player>
{
    private readonly int ForwardAnimParam = Animator.StringToHash("Forward");

    private float moveSpeed = 3; //임시 속도

    private Vector2 moveInput;

    public PlayerIdleState(IFSMEntity owner) : base(owner)
    {
    }


    public override void InitializeState()
    {
        ownerEntity.onMove = OnMove;
        ownerEntity.onLook = OnLook;
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
        var realSpeed = ownerEntity.rigidbody.velocity.magnitude;
        ownerEntity.animator.SetFloat(ForwardAnimParam, realSpeed / moveSpeed);

        var dir = ownerEntity.ConvertToCameraSpace(new Vector3(moveInput.x, 0, moveInput.y));
        ownerEntity.rigidbody.velocity = dir * moveSpeed;
    }

    public override void ClearState()
    {
    }

    private void OnMove(Vector2 input)
    {
        moveInput = input;
    }

    private void OnLook(Vector2 inout)
    {
        ownerEntity.HandleCameraRotation(inout, moveInput != Vector2.zero);
    }

    //private void HandleRotation()
    //{
    //    var dir = Camera.main.transform.forward;
    //    dir.y = 0;
    //    dir.Normalize();

    //    var look = Quaternion.LookRotation(dir);
    //    var targetRotation =
    //        Quaternion.Slerp(ownerEntity.transform.rotation, look, rotationSpeed * Time.deltaTime); // 임시 속도

    //    ownerEntity.transform.rotation = targetRotation;
    //}
}