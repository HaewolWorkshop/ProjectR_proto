using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.HenshinGrab)]
public class PlayerHenshinGrabState : PlayerMoveState
{
    protected override PlayerData data => ownerEntity.Data[1];

    private Rigidbody grabObject;

    public PlayerHenshinGrabState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        base.InitializeState();

        grabObject = null;

        var grabObjects = Physics.OverlapSphere(ownerEntity.GrabPoint.position, 0.5f, ownerEntity.GrabLayer);

        if (grabObjects.Length <= 0)
        {
            ownerEntity.RevertToPreviousState();
            return;
        }

        for (int i = 0; grabObject == null || i < grabObjects.Length; i++)
        {
            grabObject = grabObjects[0].GetComponent<Rigidbody>();
        }

        if (grabObject == null)
        {
            ownerEntity.RevertToPreviousState();
            return;
        }

        grabObject.isKinematic = true;

        ownerEntity.SetAction(Player.ButtonActions.Grab, OnGrab);

    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        grabObject.transform.position = ownerEntity.GrabPoint.transform.position;
    }

    private void OnGrab(bool isOn)
    {
        if (isOn)
        {
            grabObject.isKinematic = false;

            ownerEntity.ChangeState(Player.States.HenshinMove);
        }
    }

    public override void ClearState()
    {
        base.ClearState();

        ownerEntity.ClearAction(Player.ButtonActions.Grab);
    }
}