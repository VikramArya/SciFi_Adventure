using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public float maxLives;
    public float currentLives;
    public GameObject deathMenu;
    public Slider lifeBar;
    private CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        lifeBar.value = currentLives;
        camera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLives < 0)
        {
            deathMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            camera.canMove = false;
        }
        lifeBar.value = currentLives;

    }

    public void RemoveLives()
    {
        currentLives --;
    }
}
