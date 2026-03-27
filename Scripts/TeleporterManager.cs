using UnityEngine;
using System.Collections;
using StarterAssets;
using Cinemachine;
using Photon.Pun;

public class TeleportManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    private CharacterController controller;
    private ThirdPersonController thirdPerson;

    [Header("Locations")]
    public LocationData[] locations;
    public LocationInfoManager infoManager;

    [Header("SE")]
    public AudioClip fallSE;
    public AudioClip landSE;

    [Header("Camera Shake")]
    public CinemachineShake cameraShake;

    [Header("Camera Reset")]
    public CinemachineVirtualCamera virtualCamera;
    public float resetCameraDistance = 5f;

    private AudioSource audioSource;

    IEnumerator Start()
    {
        // Player生成待ち
        while (PhotonNetwork.LocalPlayer.TagObject == null)
        {
            yield return null;
        }

        GameObject myPlayer = PhotonNetwork.LocalPlayer.TagObject as GameObject;

        player = myPlayer.transform;
        controller = player.GetComponent<CharacterController>();
        thirdPerson = player.GetComponent<ThirdPersonController>();

        audioSource = GetComponent<AudioSource>();
    }

    public void TeleportByIndex(int index)
    {
        if (index < 0 || index >= locations.Length) return;

        LocationData loc = locations[index];
        if (loc == null || loc.teleportTarget == null) return;

        StartCoroutine(FallTeleport(loc));
    }

    IEnumerator FallTeleport(LocationData loc)
    {
        // ===== 操作停止 =====
        thirdPerson.enabled = false;
        controller.enabled = false;

        // ===== 位置変更 =====
        player.position = loc.teleportTarget.position + Vector3.up * loc.dropHeight + Random.insideUnitSphere * 1.5f;
        player.rotation = loc.teleportTarget.rotation;

        yield return null;

        controller.enabled = true;

        // ===== カメラ距離リセット =====
        ResetCameraDistance();

        // ===== 落下音 =====
        if (fallSE != null)
            audioSource.PlayOneShot(fallSE);

        float yVelocity = 0f;
        float gravity = -25f;

        while (!controller.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
            Vector3 move = new Vector3(0, yVelocity, 0);
            controller.Move(move * Time.deltaTime);
            yield return null;
        }

        // ===== 着地音 =====
        if (landSE != null)
            audioSource.PlayOneShot(landSE);

        // ===== カメラ揺れ =====
        if (cameraShake != null)
            cameraShake.Shake(1.5f, 0.3f);

        // ===== 操作復帰 =====
        thirdPerson.enabled = true;

        // ===== 初回説明表示 =====
        if (!loc.alreadyVisited)
        {
            if (infoManager != null)
                infoManager.ShowInfo(loc);

            loc.alreadyVisited = true;
        }
    }

    void ResetCameraDistance()
    {
        if (virtualCamera == null) return;

        var thirdPersonFollow =
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (thirdPersonFollow != null)
        {
            thirdPersonFollow.CameraDistance = resetCameraDistance;
        }
    }
}