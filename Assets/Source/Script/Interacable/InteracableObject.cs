using UnityEngine;

public class InteracableObject : MonoBehaviour
{
    private Animator animator;
    public bool hasInteracted = false;
    public int id;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ApplyInteracable()
    {
        animator.SetTrigger("TriggerAnim");
        hasInteracted = true;
    }

}
