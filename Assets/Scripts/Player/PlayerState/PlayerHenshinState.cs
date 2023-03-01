using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.Henshin)]
public class PlayerHenshinState : PlayerMoveState
{
    protected override float moveSpeed => ownerEntity.Data.MoveSpeed;
    
    public PlayerHenshinState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.NormalModel.gameObject.SetActive(false);
        ownerEntity.HenshinModel.gameObject.SetActive(true);

        ownerEntity.animator.avatar = ownerEntity.HenshinModel.avatar;
        
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