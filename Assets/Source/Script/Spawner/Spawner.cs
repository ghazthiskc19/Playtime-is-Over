using UnityEngine;

public class Spawner : MonoBehaviour
{
    private CameraVisibilityChecker checker;
    public bool isVisible;
    void Start()
    {
        checker = FindAnyObjectByType<CameraVisibilityChecker>();
    }
    public bool IsVisible()
    {
        isVisible = checker.IsVisible(transform);
        return isVisible;
    }
    void Update()
    {
        isVisible = checker.IsVisible(transform);
    }
}
