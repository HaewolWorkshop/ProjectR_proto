using System;
using UnityEngine;

public interface IFSMEntity
{

}

public abstract class FSMPlayer<T> : MonoBehaviour where T : IFSMEntity
{
    protected FSMState<T>[] states;

    private FSMState<T> currentState = null;
    private FSMState<T> previousState = null;
    private FSMState<T> globalState = null;

    protected virtual void Awake()
    {
        Setup();
    }

    protected virtual void Update()
    {
        globalState?.UpdateState();
        currentState?.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        globalState?.FixedUpdateState();
        currentState?.FixedUpdateState();
    }

    /// <summary>
    /// states를 초기화하고 ChangeState를 실행시키시오
    /// </summary>
    protected abstract void Setup();

    public void ChangeState(ValueType enumValue)
    {
#if UNITY_EDITOR

        var value = (int)enumValue;
        if (value < 0 || value > states.Length)
        {
            Debug.LogError($"{GetType()} : 사용할 수 없는 상태입니다. {enumValue}");
            return;
        }

#endif
        ChangeState(states[(int)enumValue]);
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

    public void SetGlobalState(FSMState<T> newState)
    {
        globalState?.ClearState();

        globalState = newState;

        globalState?.InitializeState();
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }
}
