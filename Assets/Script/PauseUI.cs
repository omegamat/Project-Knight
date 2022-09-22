using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseInterface;


    private void Start()
    {
         Pause();
    }
    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            gameIsPaused = !gameIsPaused;
            Pause();         
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT!");
    }
    public void Restart()
    {
        SceneManager.GetActiveScene();
        //SceneManager.LoadScene(1);
    }
    public void PauseSwitch()
    {
        gameIsPaused = !gameIsPaused;
        Pause();
    }

    //pause game using timescale
    void Pause()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseInterface.SetActive(true);

        }
        if (!gameIsPaused)
        {
            Time.timeScale = 1;
            pauseInterface.SetActive(false);
        }
    }
}

