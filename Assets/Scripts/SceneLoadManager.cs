using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    public GameObject DroopyGameObject;

    private GameObject persistantDataObject;
    private PersistantData persistantData;

    private bool isStarted;
    private bool isPaused;

    private void Start()
    {
        persistantDataObject = GameObject.FindGameObjectWithTag("Persistant");
        persistantData = persistantDataObject.GetComponent<PersistantData>();
        isStarted = false;
        isPaused = false;

        Time.timeScale = 0;

    }

    private void Update()
    {
        if (isStarted == true & Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                OpenPauseMenu();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;


        //set the other volume slider to the correct value
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        //Disable MainMenu
        MainMenu.SetActive(false);

        //Start Game
        DroopyGameObject.GetComponent<DroopyMovement>().isStarted = true;
        isStarted = true;
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPauseMenu()
    {
        
        //Pause game time
        Time.timeScale = 0;
        isPaused = true;

        //Activate pause menu ui
        PauseMenu.SetActive(true);

    }

    public void ResumeGame()
    {
        //Resume game
        Time.timeScale = 1;
        isPaused = false;
        //disable pause menu
        PauseMenu.SetActive(false);
    }


    public void OpenGameOverMenu()
    {
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
    }
}
