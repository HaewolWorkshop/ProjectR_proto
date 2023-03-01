using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerJumpState : FSMState<Player>
{
    private readonly LayerMask groundLayer = ~LayerMask.GetMask("Player");
    private const float groundDist = 0.2f;

    public PlayerJumpState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.animator.SetTrigger("Jump");
        ownerEntity.rigidbody.AddForce(new Vector3(0f, ownerEntity.Data.JumpPower, 0f));
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        Debug.DrawRay(ownerEntity.transform.position, Vector3.down * groundDist, Color.red);

        if (ownerEntity.rigidbody.velocity.y <= 0 &&
            Physics.Raycast(ownerEntity.transform.position, Vector3.down, out var hit, groundDist, groundLayer))
        {
            ownerEntity.ChangeState(Player.NormalStates.Move);
        }
    }

    public override void ClearState()
    {
    }
}
