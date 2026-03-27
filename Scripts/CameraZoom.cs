using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    private Cinemachine3rdPersonFollow follow;

    void Start()
    {
        follow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0f)
        {
            float distance = follow.CameraDistance;
            distance -= scroll * zoomSpeed * Time.deltaTime * 50f;
            follow.CameraDistance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }
}
