using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int) Player.States.HenshinAttackIdle)]
public class PlayerHenshinAttackState : FSMState<Player>
{
    public PlayerHenshinAttackState(IFSMEntity owner) : base(owner) { }

    public override void InitializeState()
    {
        ownerEntity.SetAction(Player.ButtonActions.Sprint, OnMove);

        if (!ownerEntity.animator.GetBool("isAttacking"))
        {
            ownerEntity.animator.SetBool("isAttacking", true);
            OnAttackMotion();
        }
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

    private void OnMove(bool isOn)
    {
        if(isOn)
        {
            ownerEntity.animator.SetBool("isAttacking", false);
            ownerEntity.ChangeState(Player.States.HenshinMove);
            Debug.Log("Changed Move");
        }
    }

    private void OnAttackMotion()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            ownerEntity.ChangeState(Player.States.HenshinSecondAttack);
        else
            ownerEntity.ChangeState(Player.States.HenshinFirstAttack);
    }


}
