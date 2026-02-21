using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TitleCinematic : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "ReadMeScene";

    [Header("UI")]
    public Image fadeImage;
    public TextMeshProUGUI titleText;

    [Header("Timing")]
    public float fadeInDuration = 1.5f;
    public float logoDuration = 3f;
    public float fadeOutDuration = 1.5f;

    [Header("Audio")]
    public AudioSource bgmSource;
    public float bgmFadeDuration = 2f;

    bool isTransitioning = false;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return StartCoroutine(FadeFromBlack());
        yield return StartCoroutine(LogoAnimation());
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeAndLoad());
    }

    void Update()
    {
        if (!isTransitioning && Input.anyKeyDown)
        {
            StopAllCoroutines();
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            SetFadeAlpha(1f - t / fadeInDuration);
            yield return null;
        }
        SetFadeAlpha(0f);
    }

    IEnumerator LogoAnimation()
    {
        bgmSource.volume = 0f;
        bgmSource.Play();

        float t = 0f;

        while (t < logoDuration)
        {
            t += Time.deltaTime;
            float progress = t / logoDuration;

            // ロゴフェードイン
            SetTextAlpha(progress);

            // ロゴ拡大（0.8 → 1.0）
            float scale = Mathf.Lerp(0.8f, 1f, progress);
            titleText.transform.localScale = Vector3.one * scale;

            // BGMフェードイン
            bgmSource.volume = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }
    }

    IEnumerator FadeAndLoad()
    {
        isTransitioning = true;

        float t = 0f;
        float startVolume = bgmSource.volume;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeOutDuration;

            SetFadeAlpha(progress);
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, progress);

            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    void SetFadeAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }

    void SetTextAlpha(float a)
    {
        Color c = titleText.color;
        c.a = a;
        titleText.color = c;
    }
}
