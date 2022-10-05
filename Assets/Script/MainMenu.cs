using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//legacy
public class MainMenu : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnLoadSceneIndex(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
