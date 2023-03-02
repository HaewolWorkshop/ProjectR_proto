using System;
using UnityEngine;

public abstract class FSMEnemy<T> : MonoBehaviour where T : IFSMEntity
{
    protected FSMState<T>[] states;

    private FSMState<T> currentState = null;
    private FSMState<T> previousState = null;

    protected virtual void Awake()
    {
        Setup();
    }
    protected abstract void Setup();

    public void ChangeState(ValueType newState)
    {
#if UNITY_EDITOR

        var value = (int)newState;
        if (value < 0 || value > states.Length)
        {
            Debug.LogError($"{GetType()} : 사용할 수 없는 상태입니다. {newState}");
            return;
        }

#endif
        ChangeState(states[(int)newState]);
    }
    private void ChangeState(FSMState<T> newState)
    {
        if (newState == null) return;

        if (currentState != null)
        {
            previousState = currentState;

            currentState.ClearState();
        }

        currentState = newState;
        currentState.InitializeState();
    }
    
    protected virtual void Update()
    {
        currentState?.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }
}