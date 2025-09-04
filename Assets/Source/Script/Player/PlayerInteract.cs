using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerController playerController;
    private bool _canInteract = true;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        EventManager.instance.OnPausedGame += HandlePause;
        EventManager.instance.OnChasingStateChanged += HandleChasing;
    }
    private void OnDisable()
    {
        EventManager.instance.OnPausedGame -= HandlePause;
        EventManager.instance.OnChasingStateChanged -= HandleChasing;
    }

    private void HandlePause(bool isPaused)
    {
        enabled = !isPaused;
    }
    private void HandleChasing(bool isChasing)
    {
        _canInteract = !isChasing; 
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (!_canInteract) return;
        InteracableObject interacableObject = collision.gameObject.GetComponent<InteracableObject>();
        if (collision.gameObject.CompareTag("Interacable Object") && !interacableObject.hasInteracted)
        {
            EventManager.instance.WhenInteractAreaChanged(true, "Press E to interact");
            if (playerController._isInteracable)
            {
                interacableObject.ApplyInteracable();
                EventManager.instance.WhenObjectInteract();
                playerController._isInteracable = false;
                EventManager.instance.WhenInteractAreaChanged(false, "");
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!_canInteract) return;
        if (collision.gameObject.CompareTag("Interacable Object"))
        {
            EventManager.instance.WhenInteractAreaChanged(false, "");
        }
    }
}
