using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        noise = virtualCamera
          .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = 0f;
    }

    public void Shake(float intensity, float time)
    {
        StartCoroutine(ShakeRoutine(intensity, time));
    }

    IEnumerator ShakeRoutine(float intensity, float time)
    {
        noise.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(time);

        noise.m_AmplitudeGain = 0f;
    }
}