using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentCells;
    public Text cellText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<PowerManager>().currentPower = currentCells;
    }

    public void AddCells(int cellsToAdd)
    {
        currentCells += cellsToAdd;
        cellText.text = "Cells : " + currentCells;
    }
}
