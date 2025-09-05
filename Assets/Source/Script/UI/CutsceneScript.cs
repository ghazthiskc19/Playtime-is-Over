using UnityEngine;
using System.Collections;
using DG.Tweening;
public class CutsceneScript : MonoBehaviour
{
    public GameObject bgcutscene;
    public RectTransform fg_rt;
    public RectTransform[] cutsceneImage;
    private RectTransform cutsceneRect;
    void Start()
    {
        cutsceneRect = bgcutscene.GetComponent<RectTransform>();
    }
    public void cutsceneAppear(int idx)
    {
        StartCoroutine(cutsceneAnimation(idx));
    }
    private IEnumerator cutsceneAnimation(int idx)
    {
        cutsceneRect.DOAnchorPos(new Vector3(-400, -200, 0), 1f);
        yield return new WaitForSeconds(0.2f);
        cutsceneImage[idx].DOAnchorPos(new Vector3(-400, -200, 0), 1f);
        yield return new WaitForSeconds(3f);
        cutsceneImage[idx].DOAnchorPos(new Vector3(400, -200, 0), 1f);
        yield return new WaitForSeconds(0.2f);
        cutsceneRect.DOAnchorPos(new Vector3(400, -200, 0), 1f);
    }   

}
