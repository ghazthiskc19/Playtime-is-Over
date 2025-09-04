using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    public TextScriptTutorial TutorialScript;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialScript.isTutorialDone)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                TransitionScript.instance.TransitionTo("GameplayScene");
            }
        }
    }
}
