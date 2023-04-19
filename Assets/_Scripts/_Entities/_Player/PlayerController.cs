using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private const float _raycastDistance = 1f;

    private Vector2 _moveDirection;
    private Vector2 _lastDirection;
    private bool _isAttackPressed;
    private bool _isMoving;

    public Vector2 MoveDirection => _moveDirection;
    public bool IsAttackPressed => _isAttackPressed;
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _inputReader.MoveEvent += InputReader_MoveEvent;
        _inputReader.InteractEvent += InputReader_InteractEvent;
        _inputReader.AttackEvent += InputReader_AttackEvent;
    }

    private void Update()
    {
        var raycastHit = Physics2D.RaycastAll(transform.position, _lastDirection, _raycastDistance);

        //TODO: add interactable detection by raycast
    }

    private void InputReader_AttackEvent(bool isPressed)
    {
        _isAttackPressed = isPressed;
    }

    private void InputReader_InteractEvent()
    {
        Debug.Log("Interact!");
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
