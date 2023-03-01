using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.Henshin)]
public class PlayerHenshinState : PlayerMoveState
{
    protected override PlayerData data => ownerEntity.Data[1];
    
    public PlayerHenshinState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.NormalModel.gameObject.SetActive(false);
        ownerEntity.HenshinModel.gameObject.SetActive(true);

        ownerEntity.animator.avatar = ownerEntity.HenshinModel.avatar;

        CameraManager.Instance.SetCamera(CameraType.Henshin3rdPerson);

        ownerEntity.ChangeState(Player.States.HenshinMove);
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