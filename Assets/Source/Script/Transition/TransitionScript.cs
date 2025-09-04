using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
public class TransitionScript : MonoBehaviour
{
    public static TransitionScript instance;
    public GameObject panelIn;
    public GameObject panelOut;
    public float duration;
    public Transform cameraTransform;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        panelIn.SetActive(false);
        panelOut.SetActive(false);
    }
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        transform.position = cameraTransform.position;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;    
    }

    public void TransitionTo(string sceneName)
    {
        StartCoroutine(OnTransitionSceneOutAndLoad(sceneName));
    }

    public IEnumerator OnTransitionSceneOutAndLoad(string sceneName)
    {
        // opsional kalau pause game pake timeScale = 0;
        Time.timeScale = 1f;
        panelOut.SetActive(true);
        SpriteRenderer sr = panelOut.GetComponent<SpriteRenderer>();
        sr.DOFade(1, duration).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(duration + 0.5f);

        // load async
        var ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
            yield return null;

    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraTransform = Camera.main.transform;
        panelIn.SetActive(true);
        SpriteRenderer sr = panelIn.GetComponent<SpriteRenderer>();
        sr.DOFade(0, duration).SetEase(Ease.OutQuint)
        .OnComplete(() =>
        {
            panelIn.SetActive(false);
            sr.DOFade(1, 0);
        });

        // hide panel out
        panelOut.SetActive(false);
        SpriteRenderer _sr = panelOut.GetComponent<SpriteRenderer>();
        _sr.DOFade(0, 0);
    } 
}
