using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPickup : MonoBehaviour
{
    public int value;
    public int healthToAdd;

    public GameObject powerPickupEffect;
    public GameObject healthPickupEffect;

    public bool isPowerCell;
    public bool isHealthCell;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isPowerCell)
        {
            FindObjectOfType<GameManager>().AddCells(value);
            Instantiate(powerPickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(other.tag == "Player" && isHealthCell)
        {
            FindObjectOfType<HealthManager>().HealPlayer(healthToAdd);
            Instantiate(healthPickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
