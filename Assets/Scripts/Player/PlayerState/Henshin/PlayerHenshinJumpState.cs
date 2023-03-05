using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.HenshinJump)]

public class PlayerHenshinJumpState : PlayerJumpState
{
    protected override PlayerData data => ownerEntity.Data[0];

    bool isFalling = false;
    float fallingStartHeight;

    public PlayerHenshinJumpState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        isFalling = ownerEntity.wtf;
        if (!isFalling)
        {
            base.InitializeState();
        }
        else
        {
            fallingStartHeight = ownerEntity.transform.position.y;
        }
    }


    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (!isFalling && ownerEntity.rigidbody.velocity.y <= 0)
        {
            isFalling = true;
            fallingStartHeight = ownerEntity.transform.position.y;
        }
    }

    protected override void OnJumpFinish()
    {
        Debug.LogWarning(Mathf.Abs(fallingStartHeight - ownerEntity.transform.position.y));

        if (Mathf.Abs(fallingStartHeight - ownerEntity.transform.position.y) > 4)
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
