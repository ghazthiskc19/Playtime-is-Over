using System.Collections;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float sprintMultiplier;
    public float staminaTime;
    public float chargingTime;
    public bool _isInteracable;
    [SerializeField] private float staminaTimeCounter;
    [SerializeField] private bool _isSprint;
    [SerializeField] private bool _isCharging;
    [SerializeField] private InputActionReference sprintAction;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private Animator animator;
    private bool _isPaused;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EventManager.instance.OnPlayerWin += OnPlayerWin;
        EventManager.instance.OnPausedGame += HandlePause;
    }
    void OnDisable()
    {
        EventManager.instance.OnPlayerWin -= OnPlayerWin;
        EventManager.instance.OnPausedGame -= HandlePause;
    }

    private void HandlePause(bool isPaused)
    {
        _isPaused = isPaused;
        animator.speed = isPaused ? 0f : 1f;

        if (isPaused)
        {
            _rb.linearVelocity = Vector2.zero; // langsung stop gerak
        }
    }
    private void Update()
    {
        if (_isPaused) return;
        bool isSprintPressed = sprintAction.action.IsPressed();
        if (isSprintPressed)
        {
            if (_isCharging)
            {
                _isSprint = false;
            }
            else
            {
                _isSprint = true;
            }

        }
        else
        {
            if (staminaTimeCounter == chargingTime)
            {
                _isCharging = false;
                return;
            }
            _isSprint = false;
            if (!_isCharging) StartCoroutine(StartCharging());
        }

        animator.SetFloat("X-Axis", _rb.linearVelocityX);
        animator.SetFloat("Y-Axis", _rb.linearVelocityY);
    }

    private void FixedUpdate()
    {
        if (_isSprint && staminaTimeCounter > 0 && !_isCharging)
        {
            _rb.linearVelocity = _moveInput * movementSpeed * sprintMultiplier;
            staminaTimeCounter -= 1f;
        }
        else
        {
            _rb.linearVelocity = _moveInput * movementSpeed;
            if (staminaTimeCounter < staminaTime) staminaTimeCounter += 0.5f;
        }
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>().normalized;
    }

    public IEnumerator StartCharging()
    {
        yield return new WaitForSeconds(chargingTime);
        _isCharging = false;
    }

    public void OnInteract(InputValue input)
    {
        if (input.isPressed)
        {
            _isInteracable = true;
        }
        else
        {
            _isInteracable = false;
        }
    }

    public void OnPlayerWin()
    {
        enabled = false;
    }   
}
