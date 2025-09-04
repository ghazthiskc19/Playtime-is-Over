using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool staminaLocked;
    public Image staminaBar;

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
            _rb.linearVelocity = Vector2.zero;
        }
    }
    private void Update()
    {
        if (_isPaused) return;
        bool isSprintPressed = sprintAction.action.IsPressed();

        if (staminaTimeCounter <= 5f)
        {
            _isSprint = false;
            staminaLocked = true;
            _isCharging = false;
        }
        else
        {
            if (staminaTimeCounter >= staminaTime * 0.5f)
            {
                staminaLocked = false;
            }

            if (isSprintPressed && !staminaLocked)
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
                if (!isSprintPressed && staminaTimeCounter < staminaTime * 0.5f)
                {
                    staminaLocked = true;
                }
                _isSprint = false;
                _isCharging = false;
            }
        }

        if (staminaBar != null)
        {
            staminaBar.fillAmount = staminaTimeCounter / staminaTime;
            if (staminaTimeCounter <= staminaTime * 0.5f)
            {
                staminaBar.color = new Color(0.5f, 0.5f, 0.5f); 
            }
            else
            {
                staminaBar.color = Color.white;
            }
        }
        animator.SetFloat("X-Axis", _rb.linearVelocityX);
        animator.SetFloat("Y-Axis", _rb.linearVelocityY);
        animator.SetBool("IsRunning", _isSprint);
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
        if (_isPaused) return;
        _moveInput = value.Get<Vector2>().normalized;
    }

    public void OnInteract(InputValue input)
    {
        if (_isPaused) return;
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
