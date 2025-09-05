using UnityEngine;
using UnityEngine.UI;

public class AudioButtonAssign : MonoBehaviour
{
    [SerializeField] private string sfxName = "tombol";
    [SerializeField] private bool preventOverlap = false;

    void Start()
    {
        Button[] allButtons = FindObjectsOfType<Button>(true);

        foreach (Button btn in allButtons)
        {
            btn.onClick.RemoveListener(() => PlayClick());
            btn.onClick.AddListener(() => PlayClick());
        }
    }
    private void PlayClick()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(sfxName, preventOverlap);
        }
    }
}