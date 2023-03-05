using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.NormalThrow)]
public class PlayerNormalThrowState : FSMState<Player>
{
    public PlayerNormalThrowState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        CameraManager.Instance.SetCamera(CameraType.NormalAim);

        ownerEntity.SetAction(Player.ButtonActions.Attack, OnAttack);
        ownerEntity.SetAction(Player.ButtonActions.Aim, OnAim);
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
    }

    private void OnAttack(bool isOn)
    {
        if (isOn)
        {

        }
    }

    private void OnAim(bool isOn)
    {
        if (!isOn)
        {
            ownerEntity.RevertToPreviousState();
        }
    }

    public override void ClearState()
    {
        CameraManager.Instance.SetCamera(CameraType.Normal3rdPerson);
    }
}