using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationInfoManager : MonoBehaviour
{
    public Image infoImage;
    public AudioSource audioSource;
    public GameObject panel;

    public void ShowInfo(LocationData loc)
    {
        if (loc.image == null && loc.narration == null)
            return;

        panel.SetActive(true);

        if (loc.image != null)
            infoImage.sprite = loc.image;

        if (loc.narration != null)
        {
            audioSource.clip = loc.narration;
            audioSource.Play();
            StartCoroutine(HideAfterVoice());
        }
    }

    IEnumerator HideAfterVoice()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        panel.SetActive(false);
    }
}