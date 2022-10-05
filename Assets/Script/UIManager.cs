using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseInterface; //pause canvas

  
    public Image HP_bar;
    public LivingBeing player;
    float maxHP = 0;
    float currentHP;

    
    void Start() 
    {
        //DOTween.defaultTimeScaleIndependent = true;
        //Pause();
        maxHP = player.maxHP;
    }

    
    void Update() // Update is called once per frame
    {
        
        currentHP = player.HP; //set healh bar
        HP_bar.fillAmount = currentHP/maxHP; //set healh life bar according with current life

        //game pause input
        if (Input.GetButtonDown("Cancel"))
        {
            //gameIsPaused = !gameIsPaused;
            PauseSwitch();       
        }
    }

    //quit game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT!");
    }

    //Load scene by index(number)
    //0 = main menu
    //1 = devroom
    //2 ... = game leves
     public void LoadSceneIndex(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
    
    //input to pause
    public void PauseSwitch()
    {
        gameIsPaused = !gameIsPaused;
        //Pause();
        StartCoroutine(Pause());
    }

    //pause game using timescale
    IEnumerator Pause()
    {
        if (gameIsPaused)
        {
            pauseInterface.transform.DOMoveX(190f,0.25f,false);
            yield return new WaitForSeconds(0.25f);
            Time.timeScale = 0;            
            //pauseInterface.SetActive(true);
            yield return null;

        }
        else
        {
            Time.timeScale = 1;
            pauseInterface.transform.DOMoveX(-200f,0.25f,false);
            yield return null;
            //pauseInterface.SetActive(false);
        }
        
    }
}
