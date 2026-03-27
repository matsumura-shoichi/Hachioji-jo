using UnityEngine;
using Cinemachine;
using System.Collections;

public class OpeningManager : MonoBehaviour
{
    public CinemachineVirtualCamera openingCam;
    public CinemachineVirtualCamera playerCam;

    public float startDistance = 100f;
    public float endDistance = 5f;
    public float duration = 5f;

    CinemachineFramingTransposer transposer;

    public void StartOpening(GameObject player)
    {
        // Follow設定
        openingCam.Follow = player.transform;
        openingCam.LookAt = player.transform;

        // Transposer取得
        transposer = openingCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        // 初期距離
        transposer.m_CameraDistance = startDistance;

        // カメラ優先度
        openingCam.Priority = 20;
        playerCam.Priority = 10;

        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float d = Mathf.Lerp(startDistance, endDistance, t / duration);
            transposer.m_CameraDistance = d;

            yield return null;
        }

        // プレイヤーカメラへ切替
        openingCam.Priority = 5;
        playerCam.Priority = 20;
    }
}