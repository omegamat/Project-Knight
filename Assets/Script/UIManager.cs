using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseUI; //pause canvas
    public GameObject deathUI;
    public TMP_Text currencyText;

  
    public Image HP_bar;
    //public GameObject player;
    public LivingBeing m_PlayerLivingBeing;
    public CharacterMovement m_PlayerCharacterMovement;
    float maxHP = 0;
    float currentHP;

    public bool isGameOver = false;

    private void Awake() 
    {
        //m_PlayerLivingBeing = GetComponent<LivingBeing>();
        //m_PlayerCharacterMovement = GetComponent<CharacterMovement>();
    }
    void Start() 
    {
        

        deathUI.SetActive(false);
        maxHP = m_PlayerLivingBeing.maxHP;
    }

    
    void Update() // Update is called once per frame
    {
        if (m_PlayerLivingBeing.HP <= 0)
            isGameOver = m_PlayerLivingBeing.GetIsAlive;

        currentHP = m_PlayerLivingBeing.HP; //set healh bar
        HP_bar.fillAmount = currentHP/maxHP; //set healh life bar according with current life
        currencyText.text = m_PlayerCharacterMovement.gems.ToString();

        //game pause input
        if (Input.GetButtonDown("Cancel"))
        {
            //gameIsPaused = !gameIsPaused;
            PauseSwitch();       
        }
        if(isGameOver)
        {
            deathUI.SetActive(true);
        }
    }

    //quit game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("GAME QUIT!");
    }

    //Load scene by index(number).
    //0 = main menu.
    //1 = devroom.
    //2 ... = game leves.
     public void LoadSceneIndex(int _sceneIndex)
    {
        Scene _scene = SceneManager.GetSceneByBuildIndex(_sceneIndex);
        SceneManager.LoadScene(_sceneIndex);
        Debug.Log("Loading..." + _scene.name);
    }
    //load Current Scene.
    public void RestartScene()
    {
        Scene _scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(_scene.name);
        Debug.Log("Loading..." + _scene.name);
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
            pauseUI.transform.DOMoveX(190f,0.25f,false);
            yield return new WaitForSeconds(0.25f);
            Time.timeScale = 0;            
            //pauseInterface.SetActive(true);
            yield return null;

        }
        else
        {
            Time.timeScale = 1;
            pauseUI.transform.DOMoveX(-200f,0.25f,false);
            yield return null;
            //pauseInterface.SetActive(false);
        }
        
    }
}
