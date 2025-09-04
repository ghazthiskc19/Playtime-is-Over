using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ArrowParentEnemy : MonoBehaviour
{
    public TMP_Text watchOutText;
    public GameObject arrow;
    public Vector3 offset;
    private Transform parentEnemy;
    private Transform transformPlayer;
    private Camera mainCam;
    CameraVisibilityChecker checker;
    SpriteRenderer _sr;
    void Start()
    {
        EventManager.instance.OnSecondMessedUp += SetTarget;
        mainCam = Camera.main;
        checker = FindAnyObjectByType<CameraVisibilityChecker>();
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        _sr = arrow.GetComponent<SpriteRenderer>();
    }
    void OnDisable()
    {
        EventManager.instance.OnSecondMessedUp -= SetTarget;
    }
    private void SetTarget()
    {
        StartCoroutine(SetTargetCoroutine());
    }
    private IEnumerator SetTargetCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        parentEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        arrow.SetActive(true);
    }
    void Update()
    {
        if (parentEnemy == null)
        {
            arrow.SetActive(false);
            return;
        }

        bool checkVisible = checker.IsVisible(parentEnemy);
        if (checkVisible)
        {
            StartCoroutine(FadeAnimationArrow());
        }
        else
        {
            arrow.SetActive(true);
            watchOutText.enabled = true;
            _sr.DOFade(1, 0.5f);
            watchOutText.DOFade(1, 0.5f);

            Vector3 enemyScreenPos = mainCam.WorldToScreenPoint(parentEnemy.position);
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Vector3 dirToEnemy = enemyScreenPos - screenCenter;
            float angle = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    private IEnumerator FadeAnimationArrow()
    {
        _sr.DOFade(0, 0.5f);
        watchOutText.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
        watchOutText.enabled = false;
    }
}
