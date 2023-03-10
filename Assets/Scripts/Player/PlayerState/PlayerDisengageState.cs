using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.Disengage)]
public class PlayerDisengageState : FSMState<Player>
{
    public PlayerDisengageState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.HenshinModel.gameObject.SetActive(false);
        ownerEntity.NormalModel.gameObject.SetActive(true);

        ownerEntity.animator.avatar = ownerEntity.NormalModel.avatar;

        CameraManager.Instance.SetCamera(CameraType.Normal3rdPerson);

        ownerEntity.ChangeState(Player.States.NormalMove);
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
}