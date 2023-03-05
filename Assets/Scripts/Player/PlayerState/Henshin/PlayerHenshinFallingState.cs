using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.HenshinFalling)]

public class PlayerHenshinFallingState : PlayerJumpState
{
    protected override PlayerData data => ownerEntity.Data[0];

    float fallingStartHeight;

    public PlayerHenshinFallingState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        var velocity = ownerEntity.rigidbody.velocity;
        moveSpeed = (new Vector2(velocity.x, velocity.z)).magnitude;
        fallingStartHeight = ownerEntity.transform.position.y;
    }

    protected override void OnJumpFinish()
    {
        if (Mathf.Abs(fallingStartHeight - ownerEntity.transform.position.y) > 5)
        {
            foreach (var obj in ownerEntity.groundObjects)
            {
                if (obj.TryGetComponent<Breakable>(out var breakable))
                {
                    breakable.Break(ownerEntity.transform.position);
                }
            }
        }

        ownerEntity.RevertToPreviousState();
    }

}
