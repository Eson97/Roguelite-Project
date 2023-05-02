using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _interactableMask;

    private const float _raycastDistance = 0.75f;

    private Vector2 _moveDirection;
    private Vector2 _lastDirection;
    private bool _isAttackPressed;
    private bool _isMoving;
    private IInteractable _selectedInteractable;

    public Vector2 MoveDirection => _moveDirection;
    public bool IsAttackPressed => _isAttackPressed;
    public bool IsMoving => _isMoving;
    public IInteractable SelectedInteractable => _selectedInteractable;

    private void Awake()
    {
        _inputReader.MoveEvent += InputReader_MoveEvent;
        _inputReader.InteractEvent += InputReader_InteractEvent;
        _inputReader.AttackEvent += InputReader_AttackEvent;
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        var raycastHit = Physics2D.Raycast(transform.position, _lastDirection, _raycastDistance, _interactableMask);

        var interactable = (raycastHit) ? raycastHit.transform.GetComponent<IInteractable>() : null;

        if (_selectedInteractable != interactable)
            SetSelectedInteractable(interactable);
    }

    private void SetSelectedInteractable(IInteractable interactable)
    {
        _selectedInteractable?.Deselect();

        _selectedInteractable = interactable;
        
        _selectedInteractable?.Select();
    }

    private void InputReader_AttackEvent(bool isPressed)
    {
        _isAttackPressed = isPressed;
    }

    private void InputReader_InteractEvent()
    {
        _selectedInteractable?.Interact();
    }

    private void InputReader_MoveEvent(Vector2 direction)
    {
        //TODO: add canMove validation

        _moveDirection = direction.normalized;

        if (_moveDirection != Vector2.zero) 
        { 
            _isMoving = true;
            _lastDirection = _moveDirection;
        }  
        else
            _isMoving = false;
    }
}
