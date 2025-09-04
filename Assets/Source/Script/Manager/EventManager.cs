using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event Action<bool> OnPausedGame;
    public event Action OnObjectInteract;
    public event Action OnFirstMessedUp;
    public event Action OnSecondMessedUp;
    public event Action OnPlayerChased;
    public event Action OnPlayerWin;
    public event Action OnEnemyLeave;
    public event Action<bool, string> OnInteractAreaChanged;
    public static EventManager instance;
    public event Action<bool> OnChasingStateChanged;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WhenObjectInteract()
    {
        OnObjectInteract?.Invoke();
    }

    public void WhenFirstMessedUp()
    {
        OnFirstMessedUp?.Invoke();
    }
    public void WhenSecondMessedUp()
    {
        OnSecondMessedUp?.Invoke();
    }
    public void WhenPlayerChased()
    {
        OnPlayerChased?.Invoke();
    }
    public void WhenPlayerWin()
    {
        OnPlayerWin?.Invoke();
    }
    public void WhenEnemyLeave()
    {
        OnEnemyLeave?.Invoke();
    }
    public void WhenInteractAreaChanged(bool status, string text)
    {
        OnInteractAreaChanged?.Invoke(status, text);
    }
    public void WhenPausedGame(bool status)
    {
        OnPausedGame?.Invoke(status);
    }
    public void WhenChasingStateChanged(bool isChasing)
    {
        OnChasingStateChanged?.Invoke(isChasing);
    }
}
