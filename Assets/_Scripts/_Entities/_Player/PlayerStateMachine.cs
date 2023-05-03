using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(PlayerController))]
public class PlayerStateMachine : MonoBehaviour
{
    #region Fields and Properties

    //[SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;
    [Header("Debug")]
    [SerializeField] private bool _logStateData;
    
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentState;
    private PlayerController _controller;
    private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;
    public PlayerController Controller => _controller;
    public float Speed => _speed;

    public PlayerState CurrentState => _currentState.State;

    #endregion

    public Action<PlayerState,PlayerState> OnBeforeStateChanged;
    public Action<PlayerState> OnStateChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();

        _factory = new(this);
    }

    private void Start()
    {
        _currentState = _factory.Idle;
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        OnBeforeStateChanged?.Invoke(CurrentState, newState.State);

        _currentState.ExitState();

        _currentState = newState;

        _currentState.EnterState();

        OnStateChanged?.Invoke(CurrentState);
    }

    public void Log(string text)
    {
        if(_logStateData)
            Debug.Log(text);
    }
}
