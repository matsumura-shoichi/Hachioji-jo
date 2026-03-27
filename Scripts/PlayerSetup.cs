using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class PlayerSetup : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine) return;

        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();

        Transform cameraRoot = transform.Find("PlayerCameraRoot");

        cam.Follow = cameraRoot;
        cam.LookAt = cameraRoot;
    }
}