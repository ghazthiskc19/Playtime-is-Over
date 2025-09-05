using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int messedUpCount;
    public int totalObjective = 5;
    public GameObject parentGameObject;
    public GameObject[] listSpawner;
    public TMP_Text objectiveText;
    public CanvasGroup objectiveGameObject;
    public CanvasGroup HUDGameObject;
    public CinemachineVirtualCamera virtualCamera;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        EventManager.instance.OnObjectInteract += IncrementMessedUp;
        EventManager.instance.OnSecondMessedUp += SpawnParent;
        listSpawner = GameObject.FindGameObjectsWithTag("Spawner");
        objectiveText.text = "0 From " + totalObjective;
        TextObjective();
    }
    void OnDisable()
    {
        EventManager.instance.OnObjectInteract -= IncrementMessedUp;
        EventManager.instance.OnSecondMessedUp -= SpawnParent;
    }
    public void IncrementMessedUp()
    {
        StartCoroutine(IncrementMessedUpCoroutine());
    }
    public IEnumerator IncrementMessedUpCoroutine()
    {
        messedUpCount++;
        objectiveText.text = messedUpCount + " From " + totalObjective;
        if (messedUpCount >= totalObjective)
        {
            yield return new WaitForSeconds(2.5f);
            EventManager.instance.WhenPlayerWin();
            EventManager.instance.WhenPausedGame(true);
            yield return null;
        }
        if (messedUpCount % 2 == 1)
        {
            EventManager.instance.WhenFirstMessedUp();
        }
        else if (messedUpCount % 2 == 0)
        {
            EventManager.instance.WhenSecondMessedUp();
        }
    }

    public void SpawnParent()
    {
        var spawnerTransform = CheckInvisibleSpawner();
        var parent = Instantiate(parentGameObject, spawnerTransform.position, Quaternion.identity, spawnerTransform);
    }

    private Transform CheckInvisibleSpawner()
    {
        foreach (var spawnerObj in listSpawner)
        {
            var spawner = spawnerObj.GetComponent<Spawner>();
            if (spawner != null && !spawner.IsVisible())
            {
                return spawner.transform;
            }
        }
        return null;
    }

    public void TextObjective()
    {
        StartCoroutine(ObjectiveAnimation());
    }

    public IEnumerator ObjectiveAnimation()
    {
        yield return new WaitForSeconds(1f);
        float duration = 1f;
        objectiveGameObject.DOFade(1, duration);
        yield return new WaitForSeconds(2f + duration);
        objectiveGameObject.DOFade(0, duration);
        yield return new WaitForSeconds(1f);
        HUDGameObject.DOFade(1, duration);
        objectiveGameObject.gameObject.SetActive(false);
    } 
}
