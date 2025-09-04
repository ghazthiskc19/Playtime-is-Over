using UnityEngine;
using System.Collections;
using DG.Tweening;
public class CutsceneScript : MonoBehaviour
{
    public GameObject bgcutscene;
    public RectTransform fg_rt;
    private RectTransform cutsceneRect;
    void Start()
    {
        cutsceneRect = bgcutscene.GetComponent<RectTransform>();
        EventManager.instance.OnObjectInteract += cutsceneAppear;
    }

    private void OnDisable() {
        EventManager.instance.OnObjectInteract -= cutsceneAppear;    
    }
    void cutsceneAppear()
    {
        StartCoroutine(cutsceneAnimation());
    }
    private IEnumerator cutsceneAnimation()
    {
        cutsceneRect.DOAnchorPos(new Vector3(-400, -200, 0), 1f);
        yield return new WaitForSeconds(0.2f);
        fg_rt.DOAnchorPos(new Vector3(-400, -200, 0), 1f);
        yield return new WaitForSeconds(3f);
        fg_rt.DOAnchorPos(new Vector3(400, -200, 0), 1f);
        yield return new WaitForSeconds(0.2f);
        cutsceneRect.DOAnchorPos(new Vector3(400, -200, 0), 1f);
    }   

}
