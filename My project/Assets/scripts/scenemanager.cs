using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenemanager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // para cargar la siguente ecena
    public void LoadNextScene()
    {
        int curretSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(curretSceneIndex + 1);
    }

    // Update is called once per frame
    public void loadfirstscene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void loadscene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
