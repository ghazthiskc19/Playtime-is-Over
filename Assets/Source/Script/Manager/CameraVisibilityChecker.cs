using UnityEngine;

public class CameraVisibilityChecker : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public bool IsVisible(Transform target)
    {
        if (cam == null) return false;

        Vector3 viewPos = cam.WorldToViewportPoint(target.position);

        return viewPos.x >= 0f && viewPos.x <= 1f &&
                viewPos.y >= 0f && viewPos.y <= 1f &&
                viewPos.z > 0f;
    }
}
