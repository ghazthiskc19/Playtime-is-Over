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
    public Image badEndingImage;
    public CanvasGroup badEndingBtn;
    [Header("True Ending panel")]
    public GameObject trueEndingPanel;
    public TMP_Text trueEndingText;
    public Image trueEndingImage;
    public CanvasGroup trueEndingBtn;
    [Header("First Messed Up")]
    private Transform transformPlayer;
    public GameObject dangerSign;
    public Vector3 offset;
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

    void BadEnding()
    {
        badEndingPanel.SetActive(true);
        Image img = badEndingPanel.GetComponent<Image>();
        img.DOFade(1f, 1f)
        .OnComplete(() =>
        {
            badEndingText.DOFade(1, 1);
            badEndingBtn.DOFade(1, 1).SetDelay(.5f);
            EventManager.instance.WhenPausedGame(true);
        });
    }
    void TrueEnding()
    {
        trueEndingPanel.SetActive(true);
        Image img = trueEndingPanel.GetComponent<Image>();
        img.DOFade(1f, 1f)
        .OnComplete(() =>
        {
            trueEndingText.DOFade(1, 1);
            trueEndingBtn.DOFade(1, 1).SetDelay(.5f);
            EventManager.instance.WhenPausedGame(true);
        });
    }


    public void FirstMessedUpText()
    {
        StartCoroutine(FirstMessedUpAnimation());
    }

    private IEnumerator FirstMessedUpAnimation()
    {
        TMP_Text text = dangerSign.GetComponent<TMP_Text>();
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
