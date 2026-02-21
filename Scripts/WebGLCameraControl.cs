using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class WebGLCameraControl : MonoBehaviour
{
    [Header("Zoom")]
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    Cinemachine3rdPersonFollow follow;

    void Start()
    {
        follow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    void Update()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        // --- ESCで解除 ---
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        // --- UIの上をクリックした場合はロックしない ---
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI())
            {
                LockCursor();
            }
        }

        // --- ロックされていなければズームしない ---
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

#endif

        Zoom();
    }

    void Zoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0f)
        {
            float dist = follow.CameraDistance;
            dist -= scroll * zoomSpeed;
            follow.CameraDistance = Mathf.Clamp(dist, minDistance, maxDistance);
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    bool IsPointerOverUI()
    {
        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject();
    }
}