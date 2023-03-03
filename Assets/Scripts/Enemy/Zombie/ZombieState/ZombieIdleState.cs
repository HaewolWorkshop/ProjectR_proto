using System.Linq;
using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Idle)]
public class ZombieIdleState : FSMState<Zombie>
{

    public ZombieIdleState(IFSMEntity owner) : base(owner)
    {
    }
    
    public override void InitializeState()
    {
        SetTargetSpot(ownerEntity.currentWanderingTargetSpot);
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
        // 시야 안에 플레이어를 탐지했다면 추적으로 전환
        if (ownerEntity.IsPlayerInSight())
        {
            ownerEntity.ChangeState(Zombie.ZombieStates.Chase);
            return;
        }

        // 방황 스팟 없으면 Idle 때 아무것도 안 함
        if (!ownerEntity.wanderSpots || !ownerEntity.wanderSpots.IsNotEmpty())
        {
            return;
        }

        var currentPosition = ownerEntity.transform.position;
        var spotCount = ownerEntity.wanderSpots.Count;
        // 현재 지정된 목표 스팟이 없는 경우 가장 가까운 거 하나 찾아줌
        if (ownerEntity.currentWanderingTargetSpot < 0)
        {
            var nearestIndex = 0;
            var nearestDistance = currentPosition.DistanceSquared(ownerEntity.wanderSpots[0].position);
            for (int i = 1; i < spotCount; ++i)
            {
                var distance = currentPosition.DistanceSquared(ownerEntity.wanderSpots[i].position);
                if (nearestDistance > distance)
                {
                    nearestIndex = i;
                    nearestDistance = distance;
                }
            }

            SetTargetSpot(nearestIndex);
        }
        var currentWanderingTargetSpot = ownerEntity.currentWanderingTargetSpot;
        // 현재 목표 스팟에 도달한 경우 && 스팟 갯수가 2개 이상인 경우
        if (ownerEntity.wanderSpots[currentWanderingTargetSpot].position.Distance(currentPosition) <= ownerEntity.wanderSpotRange && spotCount > 1)
        {
            switch (ownerEntity.wanderType)
            {
                // 지정된 경로를 계속 순환함
                case Zombie.WanderType.CycleByOrder:
                    {
                        SetTargetSpot((currentWanderingTargetSpot + 1) % spotCount);
                    }
                    break;
                case Zombie.WanderType.Random:
                    {
                        // 랜덤으로 돌림
                        SetTargetSpot(Random.Range(0, spotCount));
                    }
                    break;
            }
        }
    }

    private void SetTargetSpot(int index)
    {
        if (!ownerEntity.wanderSpots 
            || !ownerEntity.wanderSpots.IsNotEmpty() 
            || index < 0 || index >= ownerEntity.wanderSpots.Count
        )
        {
            return;   
        }
        ownerEntity.currentWanderingTargetSpot = index;
        ownerEntity.SetMoveTarget(ownerEntity.wanderSpots[index].position);
    }

    public override void ClearState()
    {
    }
}