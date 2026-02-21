using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReadmeIntroPlayer : MonoBehaviour
{
    public CanvasGroup readmeCanvas;
    public CanvasGroup introCanvas;
    public AudioSource narrationAudio;
    public GameObject introPanel;

    public float fadeTime = 2f;
    public string nextSceneName = "GameScene";

    Coroutine introCoroutine;

    void Start()
    {
        introPanel.SetActive(false);   // © Å‰‚ÉŠ®‘S’âŽ~
        introCanvas.alpha = 0;
        narrationAudio.Stop();
    }

    public void StartIntro()
    {
        introPanel.SetActive(true);    // © ‚±‚±‚Å‰‚ß‚ÄoŒ»
        introCoroutine = StartCoroutine(PlayIntro());
    }

    public void SkipIntro()
    {
        if (introCoroutine != null)
            StopCoroutine(introCoroutine);

        narrationAudio.Stop();
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator PlayIntro()
    {
        readmeCanvas.alpha = 0;
        readmeCanvas.interactable = false;
        readmeCanvas.blocksRaycasts = false;

        yield return StartCoroutine(Fade(0, 1));

        narrationAudio.Play();

        yield return new WaitWhile(() => narrationAudio.isPlaying);

        yield return StartCoroutine(Fade(1, 0));

        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            introCanvas.alpha = Mathf.Lerp(start, end, t / fadeTime);
            yield return null;
        }
        introCanvas.alpha = end;
    }
}