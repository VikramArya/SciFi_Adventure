using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public HealthManager theHealthManager;
    public Renderer renderer;

    public Material inactiveCheckpoint;
    public Material activeCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        theHealthManager = FindObjectOfType<HealthManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointActive()
    {
        Checkpoints[] checkpoints = FindObjectsOfType<Checkpoints>();
        foreach(Checkpoints cp in checkpoints)
        {
            cp.CheckpointInactive();
        }
        renderer.material = activeCheckpoint;
    }

    public void CheckpointInactive()
    {
        renderer.material = inactiveCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            theHealthManager.SetSpawnPoint(transform.position);
            CheckpointActive();
        }
    }
}
