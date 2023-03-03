using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.Henshin)]
public class PlayerHenshinState : FSMState<Player>
{
    public PlayerHenshinState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        CameraManager.Instance.SetCamera(CameraType.Henshin3rdPerson);


        time = 0;
        step = 0;
    }


    // 아 모르겠다~~~~

    private float poseSpeed = 5;

    private float time;
    private int step;

    private Material normalMat;
    private Material henshinMat;

    public override void UpdateState()
    {
        switch (step)
        {
            case 0: // 머터리얼 변경

                normalMat = ownerEntity.NormalModel.gameObject.GetComponentInChildren<Renderer>().material;
                henshinMat = ownerEntity.HenshinModel.gameObject.GetComponentInChildren<Renderer>().material;

                ownerEntity.NormalModel.gameObject.GetComponentInChildren<Renderer>().material = ownerEntity.henshinMat;
                ownerEntity.HenshinModel.gameObject.GetComponentInChildren<Renderer>().material = ownerEntity.henshinMat;

                step++;

                break;
            case 1:// 포즈 취함

                ownerEntity.animator.SetLayerWeight(1, time * poseSpeed);

                if (time * poseSpeed >= 1)
                {
                    ownerEntity.animator.SetLayerWeight(1, 1);
                    time = 0;
                    step++;
                }
                break;

            case 2: // 대기
                if (time >= 0.1f)
                {
                    time = 0;
                    step++;
                }
                break;

            case 3: // 모델 변경

                ownerEntity.NormalModel.gameObject.SetActive(false);
                ownerEntity.HenshinModel.gameObject.SetActive(true);

                var stateInfo = ownerEntity.animator.GetCurrentAnimatorStateInfo(0);

                ownerEntity.animator.avatar = ownerEntity.HenshinModel.avatar;
                ownerEntity.animator.Play(stateInfo.shortNameHash, 0, stateInfo.normalizedTime);

                ownerEntity.animator.Play("Pose", 1, 1);

                step++;
                break;

            case 4: // 대기
                if (time >= 0.1f)
                {
                    time = 0;
                    step++;
                }
                break;

            case 5:

                ownerEntity.animator.SetLayerWeight(1, 1 - time * poseSpeed);

                if (time * poseSpeed >= 1)
                {
                    ownerEntity.animator.SetLayerWeight(1, 0);
                    time = 0;
                    step++;
                }

                break;
            case 6:

                ownerEntity.NormalModel.gameObject.GetComponentInChildren<Renderer>().material = normalMat;
                ownerEntity.HenshinModel.gameObject.GetComponentInChildren<Renderer>().material = henshinMat;

                ownerEntity.ChangeState(Player.States.HenshinMove);
                step++;
                break;
        }


        time += Time.deltaTime;
    }

    public override void FixedUpdateState()
    {
    }

    public override void ClearState()
    {
    }
}