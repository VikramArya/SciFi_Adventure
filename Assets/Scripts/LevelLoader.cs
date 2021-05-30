using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{   
    public string levelToLoad;
    public Slider loadingBar;
    public GameObject loadingScreen;
    public Text progressText;

    public void OnTriggerEnter(Collider other)  
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadLevel());
        }
    }  

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            progressText.text = progress + "%";
            yield return null; 
        }
    }
}
