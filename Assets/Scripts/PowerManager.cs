using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public GameObject portal;
    private GameManager theGameManager;
    public Slider powerBar;
    public int powerRequired;
    public int currentPower;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GetComponent<GameManager>();
        currentPower = theGameManager.currentCells;
    }

    // Update is called once per frame
    void Update()
    {   currentPower = theGameManager.currentCells;

        if(currentPower >= powerRequired)
        {
            portal.SetActive(true);
        }   
        powerBar.value = currentPower;
    }

    public void AddPower()
    {
        currentPower = theGameManager.currentCells;
    }
}
