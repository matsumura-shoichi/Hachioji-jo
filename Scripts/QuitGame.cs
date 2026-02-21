using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public GameObject exitPanel;

    public void ShowExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void HideExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
