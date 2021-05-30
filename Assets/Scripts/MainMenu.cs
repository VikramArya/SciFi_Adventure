using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public string controlsLevel;
    public string mainMenu;
    public GameObject pauseMenu;
    public Slider loadingBar;
    public GameObject loadingScreen;
    public Text progressText;
    private CameraController camera;

    void Start()
    {
        camera = FindObjectOfType<CameraController>();
    }

    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            camera.canMove = false;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }*/

    public void Play()
    {
        StartCoroutine(PlayCo());
    }

    IEnumerator PlayCo()
    {
        AsyncOperation playOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while(!playOperation.isDone)
        {
            float progress1 = Mathf.Clamp01(playOperation.progress / .9f);
            loadingBar.value = progress1;
            progressText.text = progress1 + "%";
            yield return null; 
        }
    }

    public void Controls()
    {
        SceneManager.LoadScene(controlsLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void QuitToMainMenu()
    {
        StartCoroutine(QuitToMainMenuCo());
    }

    IEnumerator QuitToMainMenuCo()
    {
        AsyncOperation quitToMainOperation = SceneManager.LoadSceneAsync(mainMenu);
        camera.canMove = true;
        Time.timeScale = 1f;
        loadingScreen.SetActive(true);
        while(!quitToMainOperation.isDone)
        {
            float progress2 = Mathf.Clamp01(quitToMainOperation.progress / .9f);
            loadingBar.value = progress2;
            progressText.text = progress2 + "%";
            yield return null; 
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        camera.canMove = true;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        StartCoroutine(QuitToMainMenuCo());
    }

    IEnumerator RestartGameCo()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        camera.canMove = true;
        AsyncOperation restartOperation = SceneManager.LoadSceneAsync(Application.loadedLevel);
        loadingScreen.SetActive(true);
        while(!restartOperation.isDone)
        {
            float progress3 = Mathf.Clamp01(restartOperation.progress / .9f);
            loadingBar.value = progress3;
            progressText.text = progress3 + "%";
            yield return null; 
        }
    }    

}
