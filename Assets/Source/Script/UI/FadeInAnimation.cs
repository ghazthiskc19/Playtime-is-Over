using UnityEngine;
using DG.Tweening;
using System.Collections;
public class FadeAnimation : MonoBehaviour
{
    public GameObject targetPanel;
    public void targetFadeInAnimation()
    {
        StartCoroutine(FadeOutAnimation());
    }
    public IEnumerator FadeOutAnimation()
    {
        CanvasGroup cg = targetPanel.GetComponent<CanvasGroup>();
        cg.DOFade(0, .5f);
        yield return new WaitForSeconds(.5f);
        targetPanel.SetActive(false);
    }
}
