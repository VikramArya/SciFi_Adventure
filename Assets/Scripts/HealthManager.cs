using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public float invincibilityLength;
    private float invincibilityCounter;

    public Slider healthbar;
    public PlayerController thePlayer;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;
    public GameObject respawnEffect;

    private LifeManager theLifeManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = thePlayer.transform.position;
        theLifeManager = GetComponent<LifeManager>(); 
        //thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = currentHealth;
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                theLifeManager.RemoveLives();
                Respawn();
            } 
            else
            {
                thePlayer.Knockback(direction);
                invincibilityCounter = invincibilityLength;
            }
        }
    }

    public void Respawn()
    {
        //thePlayer.transform.position = respawnPoint;
        //currentHealth = maxHealth;   
        if(!isRespawning)
        {
            StartCoroutine("RespawnCo");  
        }     
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true; 
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;
        thePlayer.transform.position = respawnPoint;
        Instantiate(respawnEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        thePlayer.gameObject.SetActive(true);
        currentHealth = maxHealth;
        invincibilityCounter = invincibilityLength;  
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
