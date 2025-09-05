using UnityEngine;

public class InteracableObject : MonoBehaviour
{
    private CutsceneScript cs;
    private Animator animator;
    public bool hasInteracted = false;
    public int id;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cs = FindAnyObjectByType<CutsceneScript>();
    }

    public void ApplyInteracable()
    {
        animator.SetTrigger("TriggerAnim");
        hasInteracted = true;
        AudioManager.instance.PlaySFXById(id);
        cs.cutsceneAppear(id);
    }

}
