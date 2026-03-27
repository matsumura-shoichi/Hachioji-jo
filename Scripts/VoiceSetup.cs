using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

public class VoiceSetup : MonoBehaviourPun
{
    void Start()
    {
        // 🎤 送信設定（自分だけ話す）
        Recorder recorder = GetComponent<Recorder>();
        if (recorder != null)
        {
            recorder.TransmitEnabled = photonView.IsMine;
        }

        // 🔇 自分の声をミュート（木魂防止）
        if (photonView.IsMine)
        {
            AudioSource audioSource = GetComponentInChildren<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = true;
            }
        }
    }
}