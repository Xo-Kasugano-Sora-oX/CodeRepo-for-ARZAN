using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
[CreateAssetMenu(menuName = "Player Input")]
public class PLAYERinput : ScriptableObject, NewActions.IGamePlayerActions
{
    NewActions inputActions;
    public event System.Action<Vector2> Wcnmd = delegate {};//onMove
    public event System.Action sbZhaodi = delegate {};//onStopMove
    public event System.Action sbzhaoDi = delegate {};//onFire
    public event System.Action Sbzhaodi = delegate {};//onStopFire
    void OnEnable()
    {
        inputActions = new NewActions();

        inputActions.GamePlayer.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInput();
    }
    public void EnableAllInputs()
    {
        inputActions.GamePlayer.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableAllInput()
    {
        inputActions.GamePlayer.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Wcnmd.Invoke(context.ReadValue<Vector2>());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            sbZhaodi.Invoke();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            
        }

    }
}
