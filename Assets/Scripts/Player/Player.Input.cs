using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public partial class Player
{


    public enum ButtonActions // bool 값으로 입력받는 액션
    {
        Attack,
        Guard,
        Jump,
        Dash,
        Skill1,
        Skill2
    }

    public enum ValueActions //float 값으로 입력받는 액션
    {
    }


    // 벡터넘겨주는 액션은 따로 처리
    public UnityAction<Vector2> onMove { get; set; }
    private InputVector2Damper moveInputDamper = new InputVector2Damper();

    public UnityAction<Vector2> onLook { get; set; }


    private Dictionary<ButtonActions, InputAction> buttonActions;
    private Dictionary<ButtonActions, UnityAction<bool>> buttonEvents;

    private Dictionary<ValueActions, InputAction> valueActions;
    private Dictionary<ValueActions, UnityAction<bool>> valueEvents;


    private PlayerInputActions inputActions;
    private InputAction moveInputAction;
    private InputAction lookInputAction;

    private void InitInputs()
    {
        buttonActions = new Dictionary<ButtonActions, InputAction>();
        buttonEvents = new Dictionary<ButtonActions, UnityAction<bool>>();

        valueActions = new Dictionary<ValueActions, InputAction>();
        valueEvents = new Dictionary<ValueActions, UnityAction<bool>>();

        inputActions = new global::PlayerInputActions();

        moveInputAction = inputActions.Player.Move;
        lookInputAction = inputActions.Player.Look;

        buttonActions.Add(ButtonActions.Jump, inputActions.Player.Jump);
        inputActions.Player.Jump.started += (x) => GetAction(ButtonActions.Jump)?.Invoke(true);
        inputActions.Player.Jump.canceled += (x) => GetAction(ButtonActions.Jump)?.Invoke(false);

        // 임시
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected void UpdateInputs()
    {
        var moveInput = moveInputDamper.getDampedValue(moveInputAction.ReadValue<Vector2>(), Time.deltaTime);
        var lookInput = lookInputAction.ReadValue<Vector2>();

        onMove?.Invoke(moveInput);
        onLook?.Invoke(lookInput);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    private UnityAction<bool> GetAction(ButtonActions type)
    {
        if (buttonEvents.TryGetValue(type, out var action))
        {
            return action;
        }
        return null;
    }

    private UnityAction<bool> GetAction(ValueActions type)
    {
        if (valueEvents.TryGetValue(type, out var action))
        {
            return action;
        }
        return null;
    }
    
    public bool GetActionValue(ButtonActions type)
    {
        if (buttonActions.TryGetValue(type, out var input))
        {
            return input.IsPressed();
        }
        return false;
    }

    public float GetActionValue(ValueActions type)
    {
        if (valueActions.TryGetValue(type, out var input))
        {
            return input.ReadValue<float>();
        }
        return 0;
    }
    
    public void SetAction(ButtonActions type, UnityAction<bool> action, bool update = true)
    {
        if (!buttonEvents.ContainsKey(type))
        {
            buttonEvents.Add(type, action);
        }
        else
        {
            buttonEvents[type] = action;
        }

        if (update && buttonActions.TryGetValue(type, out var input))
        {
            action?.Invoke(input.IsPressed());
        }
    }

    public void ClearAction(ButtonActions type)
    {
        if (buttonEvents.ContainsKey(type))
        {
            buttonEvents.Remove(type);
        }
    }
}