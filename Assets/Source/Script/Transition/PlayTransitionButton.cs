using UnityEngine;

public class PlayTransitionButton : TransitionButton
{
    protected override void OnClick()
    {
        Debug.Log(PlayerPrefs.GetInt("HasPlayed"));
        if (PlayerPrefs.GetInt("HasPlayed", 0) == 0)
        {

            TransitionScript.instance.TransitionTo("TutorialScene");
        }
        else
        {
            TransitionScript.instance.TransitionTo("GameplayScene");
        }
    }

    [ContextMenu("Delete Player Prefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
