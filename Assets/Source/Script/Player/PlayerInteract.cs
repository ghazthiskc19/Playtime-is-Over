using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerController playerController;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        EventManager.instance.OnPausedGame += HandlePause;
    }
    private void OnDisable()
    {
        EventManager.instance.OnPausedGame -= HandlePause;
    }

    private void HandlePause(bool isPaused)
    {
        enabled = !isPaused;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        InteracableObject interacableObject = collision.gameObject.GetComponent<InteracableObject>();
        if (collision.gameObject.CompareTag("Interacable Object") && !interacableObject.hasInteracted)
        {
            EventManager.instance.WhenInteractAreaChanged(true, "Press E to interact");
            if (playerController._isInteracable)
            {
                interacableObject.ApplyInteracable();
                EventManager.instance.WhenObjectInteract();
                playerController._isInteracable = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interacable Object"))
        {
            EventManager.instance.WhenInteractAreaChanged(false, "");
        }
    }
}
