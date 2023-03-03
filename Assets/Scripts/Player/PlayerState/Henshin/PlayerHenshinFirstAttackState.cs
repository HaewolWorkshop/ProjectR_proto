using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int)Player.States.HenshinFirstAttack)]
public class PlayerHenshinFirstAttackState : FSMState<Player>
{
    private const int AttackAnimKey = 1;
    private const float AnimEndTime = 0.9f;

    public PlayerHenshinFirstAttackState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.SetHitBox(true);
        ownerEntity.animator.SetInteger("isAttackMotion", AttackAnimKey);

    }

    public override void UpdateState()
    {
        if (ownerEntity.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack1") &&
            ownerEntity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > AnimEndTime)
        {
            ownerEntity.RevertToPreviousState();
        }
    }

    public override void FixedUpdateState()
    {
    }
    public override void ClearState()
    {
        Debug.Log("CLEAR");
        ownerEntity.SetHitBox(false);
        ownerEntity.animator.SetInteger("isAttackMotion", 0);
    }

}
