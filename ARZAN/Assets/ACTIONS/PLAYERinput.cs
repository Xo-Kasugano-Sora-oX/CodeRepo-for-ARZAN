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
    public event System.Action<Vector2> Wcnmd = delegate {};
    public event System.Action sbZhaodi = delegate {};
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
}
