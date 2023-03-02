using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.Henshin)]
public class PlayerHenshinState : FSMState<Player>
{
    public PlayerHenshinState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.NormalModel.gameObject.SetActive(false);
        ownerEntity.HenshinModel.gameObject.SetActive(true);

        var stateInfo = ownerEntity.animator.GetCurrentAnimatorStateInfo(0);

        ownerEntity.animator.avatar = ownerEntity.HenshinModel.avatar;
        ownerEntity.animator.Play(stateInfo.shortNameHash, 0, stateInfo.normalizedTime);

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