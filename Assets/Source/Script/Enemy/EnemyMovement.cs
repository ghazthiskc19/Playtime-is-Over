using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float timeBeforeLeave;
    public float movementSpeed;
    public Transform chaseTarget;
    public Vector3 _firstPosition;
    private NavMeshAgent agent;
    [SerializeField] private bool _isChasing;
    [SerializeField] private bool _isReturning;
    [SerializeField] private float chaseTimer;
    private float updateRate = 0.3f;
    private float nextUpdateTime = 0f;
    private Animator animator;
    private bool _isPaused;
    CameraVisibilityChecker checker;

    [Header("UI or HUD Related To Enemy")]
    public TMP_Text timerEnemyText;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        checker = FindAnyObjectByType<CameraVisibilityChecker>();
        _firstPosition = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = movementSpeed;

        chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;
        chaseTimer = timeBeforeLeave;
        _isChasing = true;

        _isReturning = false;
        EventManager.instance.OnPausedGame += HandlePause;
    }
    private void OnDisable()
    {
        EventManager.instance.OnPausedGame -= HandlePause;
    }
    private void HandlePause(bool isPaused)
    {
        if (isPaused)
        {
            _isPaused = true;
            agent.isStopped = true;
            animator.speed = 0f; 
        }
        else
        {
            _isPaused = false;
            agent.isStopped = false;
            animator.speed = 1f;
        }
    }
    void Update()
    {
        if (_isPaused) return;
        if (_isChasing)
        {
            EventManager.instance.WhenChasingStateChanged(true);
            StartCoroutine(ChasePlayer());
            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                _isChasing = false;
                EventManager.instance.WhenChasingStateChanged(false);
                StartCoroutine(DelayBeforeReturn(2f));
            }
        }
        else if (_isReturning)
        {
            ReturnToSpawn();
        }

        animator.SetFloat("X-Axis", agent.velocity.x);
    }
    private IEnumerator DelayBeforeReturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isReturning = true;
    }
    private IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(0.5f);

        if (Time.time >= nextUpdateTime)
        {
            agent.SetDestination(chaseTarget.position);
            nextUpdateTime = Time.time + updateRate;
        }
    }

    private void  ReturnToSpawn()
    {
        if (!checker.IsVisible(gameObject.transform))
        {
            Destroy(gameObject);
        }
        agent.SetDestination(_firstPosition);
        agent.speed = movementSpeed * 3;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            _isReturning = false;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.instance.WhenPlayerChased();
        }
    }
}
