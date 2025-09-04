using UnityEngine;
using DG.Tweening;
public class FadeOutAnimation : MonoBehaviour
{
    public GameObject targetPanel;
    public void targetFadeOutAnimation()
    {
        targetPanel.SetActive(true);
        CanvasGroup cg = targetPanel.GetComponent<CanvasGroup>();
        cg.DOFade(1, .5f);
    }
}
