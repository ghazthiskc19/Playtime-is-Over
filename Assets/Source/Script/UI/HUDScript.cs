using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    public TMP_Text interactText;
    [Header("Bad Ending panel")]
    public GameObject badEndingPanel;
    public TMP_Text badEndingText;
    public CanvasGroup badEndingBtn;
    [Header("True Ending panel")]
    public GameObject trueEndingPanel;
    public TMP_Text trueEndingText;
    public CanvasGroup trueEndingBtn;
    [Header("First Messed Up")]
    private Transform transformPlayer;
    public GameObject dangerSign;
    public Vector3 offset;
    public CanvasGroup[] bandEndingSprite;
    public CanvasGroup[] trueEndingSprite;
    void Start()
    {
        EventManager.instance.OnInteractAreaChanged += HandleInteractUI;
        EventManager.instance.OnPlayerChased += BadEnding;
        EventManager.instance.OnPlayerWin += TrueEnding;
        EventManager.instance.OnFirstMessedUp += FirstMessedUpText;
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (transformPlayer != null && dangerSign != null)
        {
            RectTransform rectDangerSign = dangerSign.GetComponent<RectTransform>();
            rectDangerSign.position = transformPlayer.position + offset;
        }
    }

    void OnDisable()
    {
        EventManager.instance.OnInteractAreaChanged -= HandleInteractUI;
        EventManager.instance.OnPlayerChased -= BadEnding;
        EventManager.instance.OnPlayerWin -= TrueEnding;
        EventManager.instance.OnFirstMessedUp -= FirstMessedUpText;
    }

    void HandleInteractUI(bool state, string text)
    {
        interactText.gameObject.SetActive(state);
        if (state) interactText.text = text;
    }
    [ContextMenu("Bad Ending")]
    void BadEnding()
    {
        badEndingBtn.alpha = 0;
        badEndingBtn.interactable = false;
        badEndingBtn.blocksRaycasts = false;
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM("anak di tangkap ibu");
        badEndingPanel.SetActive(true);
        Image img = badEndingPanel.GetComponent<Image>();
        img.DOFade(1f, 1f)
        .OnComplete(() =>
        {
            EventManager.instance.WhenPausedGame(true);
            StartCoroutine(SceneAnimation(bandEndingSprite, badEndingText, badEndingBtn));
        });
    }
    [ContextMenu("Happy Ending")]
    void TrueEnding()
    {
        trueEndingBtn.alpha = 0;
        trueEndingBtn.interactable = false;
        trueEndingBtn.blocksRaycasts = false;
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM("anak tidur edited");
        trueEndingPanel.SetActive(true);
        Image img = trueEndingPanel.GetComponent<Image>();
        img.DOFade(1f, 1f)
        .OnComplete(() =>
        {
            EventManager.instance.WhenPausedGame(true);
            StartCoroutine(SceneAnimation(trueEndingSprite, trueEndingText, trueEndingBtn));
        });
    }
    private IEnumerator SceneAnimation(CanvasGroup[] listSprite, TMP_Text endingText, CanvasGroup endingButton)
    {
        float fadeDuration = 1f;
        float stayDuration = 2f;

        for (int i = 0; i < listSprite.Length; i++)
        {
            CanvasGroup cg = listSprite[i];
            if (cg == null) continue;
            cg.alpha = 0;
            cg.gameObject.SetActive(true);

            yield return cg.DOFade(1f, fadeDuration).WaitForCompletion();
            yield return new WaitForSeconds(stayDuration);

            if (i < listSprite.Length - 1)
            {
                yield return cg.DOFade(0f, fadeDuration).WaitForCompletion();
                cg.gameObject.SetActive(false);
            }
        }
        endingText.DOFade(1, 1);

        endingButton.alpha = 0;
        endingButton.DOFade(1, 1).SetDelay(.5f)
        .OnComplete(() =>
        {
            endingButton.interactable = true;
            endingButton.blocksRaycasts = true;
        });
    }

    public void FirstMessedUpText()
    {
        StartCoroutine(FirstMessedUpAnimation());
    }

    private IEnumerator FirstMessedUpAnimation()
    {
        Image text = dangerSign.GetComponent<Image>();
        text.enabled = true;
        yield return new WaitForSeconds(.1f);
        text.DOFade(1, 0.4f);
        yield return new WaitForSeconds(2f);
        text.DOFade(0, 0.4f)
        .OnComplete(() =>
        {
            text.enabled = false;
        });
    }
}
