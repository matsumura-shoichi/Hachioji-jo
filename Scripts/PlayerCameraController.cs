using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerCameraController : MonoBehaviourPun
{
    public CinemachineVirtualCamera virtualCamera;

    [Header("Zoom")]
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    private Cinemachine3rdPersonFollow follow;

    void Start()
    {
        // 自分のプレイヤー以外はカメラ無効
        if (!photonView.IsMine)
        {
            if (virtualCamera != null)
                virtualCamera.gameObject.SetActive(false);

            return;
        }

        // Cinemachine取得
        if (virtualCamera != null)
        {
            follow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (follow == null) return;

        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0f)
        {
            float distance = follow.CameraDistance;
            distance -= scroll * zoomSpeed * 50f * Time.deltaTime;
            follow.CameraDistance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }
}