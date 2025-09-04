using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int messedUpCount;
    public int totalObjective = 5;
    public GameObject parentGameObject;
    public GameObject[] listSpawner;
    public TMP_Text objectiveText;
    void Start()
    {
        EventManager.instance.OnObjectInteract += IncrementMessedUp;
        EventManager.instance.OnSecondMessedUp += SpawnParent;
        listSpawner = GameObject.FindGameObjectsWithTag("Spawner");
        objectiveText.text = "0 From " + totalObjective;
    }
    void OnDisable()
    {
        EventManager.instance.OnObjectInteract -= IncrementMessedUp;
        EventManager.instance.OnSecondMessedUp -= SpawnParent;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void IncrementMessedUp()
    {
        messedUpCount++;
        objectiveText.text = messedUpCount + " From " + totalObjective;
        if (messedUpCount >= totalObjective)
        {
            EventManager.instance.WhenPlayerWin();
            return;
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
}
