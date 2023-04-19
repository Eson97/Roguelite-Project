using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameInput;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, IGameplayActions, IUIActions
{
    private GameInput _gameInput;

    public event Action<Vector2> MoveEvent;
    public event Action<bool> AttackEvent;
    public event Action InteractEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;


    private void OnEnable()
    {
        if (_gameInput != null) return;

        _gameInput = new GameInput();

        _gameInput.Gameplay.SetCallbacks(this);
        _gameInput.UI.SetCallbacks(this);

        SetGameplay();
    }

    public void SetGameplay()
    {
        _gameInput.UI.Disable();
        _gameInput.Gameplay.Enable();
    }
    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    #region Gameplay Actions

    public void OnMove(CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

    public void OnAttack(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            AttackEvent?.Invoke(true);
        else
            AttackEvent?.Invoke(false);
    }
    
    public void OnInteract(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            InteractEvent?.Invoke();
    }

    public void OnPause(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    #endregion

    #region UI Actions

    public void OnResume(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }

    #endregion
}
