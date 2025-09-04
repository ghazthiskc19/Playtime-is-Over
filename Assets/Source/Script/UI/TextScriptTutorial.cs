using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class TextScriptTutorial : MonoBehaviour
{
    public GameObject panelTutorial;
    public TMP_Text[] allText;
    public TMP_Text UIText;
    public int idxCounter;
    public float typingSpeed = 0.05f;
    public bool isTutorialDone;
    [SerializeField] private bool _isTyping;
    private Button button;
    private Coroutine typingCoroutine;

    void Start()
    {
        button = GetComponent<Button>();
        idxCounter = 0;
        button.onClick.AddListener(NextText);
        isTutorialDone = false;
        NextText();
    }
    void Update()
    {
        if (_isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                UIText.text = allText[idxCounter - 1].text;
                _isTyping = false;
            }
        }
    }

    public void NextText()
    {
        if (_isTyping) return;
        if (idxCounter >= allText.Length)
        {
            PlayerPrefs.SetInt("HasPlayed", 1);
            PlayerPrefs.Save();
            panelTutorial.SetActive(false);
            isTutorialDone = true;
        }

        UIText.text = "";
        typingCoroutine = StartCoroutine(ShowText(allText[idxCounter].text));
        idxCounter++;
    }

    public IEnumerator ShowText(string target)
    {
        _isTyping = true;
        foreach (char c in target)
        {
            UIText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        _isTyping = false;
    }
    
}
