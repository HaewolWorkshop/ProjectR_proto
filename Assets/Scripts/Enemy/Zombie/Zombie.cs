using System;
using UnityEngine;

public class Zombie : FSMEnemy<Zombie>, IFSMEntity
{
    public enum ZombieStates : int
    {
        Idle = 0,
        Chase,
        Attack,
        Dead,
        
        Max
    }
    
    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }
    
    protected override void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        states[(int) ZombieStates.Idle] = new ZombieIdleState(this);
    
        ChangeState(ZombieStates.Idle);
    }

    public void MoveTo(Vector3 position)
    {
        
    }
}