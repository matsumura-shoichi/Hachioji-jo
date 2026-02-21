using UnityEngine;
using System.Collections;
using StarterAssets;

public class TeleportManager : MonoBehaviour
{
    public Transform player;
    private CharacterController controller;
    private ThirdPersonController thirdPerson;

    public LocationData[] locations;
    public LocationInfoManager infoManager;

    [Header("SE")]
    public AudioClip fallSE;
    public AudioClip landSE;

    [Header("Camera Shake")]
    public CameraShake cameraShake;

    private AudioSource audioSource;

    void Start()
    {
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
        // 操作停止
        thirdPerson.enabled = false;

        controller.enabled = false;

        player.position = loc.teleportTarget.position + Vector3.up * loc.dropHeight;
        player.rotation = loc.teleportTarget.rotation;

        yield return null;

        controller.enabled = true;

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

        // 着地音
        if (landSE != null)
            audioSource.PlayOneShot(landSE);

        // カメラ揺れ
        if (cameraShake != null)
            cameraShake.Shake();

        // 操作復帰
        thirdPerson.enabled = true;

        // 情報表示
        // ★初回だけ説明を出す
        if (!loc.alreadyVisited)
        {
            if (infoManager != null)
                infoManager.ShowInfo(loc);

            loc.alreadyVisited = true;
        }
    }
}