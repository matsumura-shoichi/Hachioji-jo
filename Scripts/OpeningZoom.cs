using UnityEngine;
using Cinemachine;
using System.Collections;

public class OpeningZoom : MonoBehaviour
{
    public CinemachineVirtualCamera openingCam;
    public CinemachineVirtualCamera playerCam;

    public float startDistance = 100f;
    public float endDistance = 5f;
    public float duration = 5f;

    CinemachineFramingTransposer transposer;

    void Start()
    {
        transposer = openingCam
        .GetCinemachineComponent<CinemachineFramingTransposer>();

        transposer.m_CameraDistance = startDistance;

        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float d = Mathf.Lerp(
                startDistance,
                endDistance,
                t / duration
            );

            transposer.m_CameraDistance = d;

            yield return null;
        }

        // プレイヤーカメラへ切替
        openingCam.Priority = 5;
        playerCam.Priority = 20;
    }
}