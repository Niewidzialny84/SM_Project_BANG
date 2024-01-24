using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public void ChangeScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
