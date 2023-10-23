using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollision : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered) 
        {
            CheckpointData playersCheckpointData = other.GetComponent<DeathSystem>().currentCheckpoint.GetComponent<CheckpointData>();
            if (playersCheckpointData.checkpointNumber == this.gameObject.GetComponent<CheckpointData>().checkpointNumber - 1) 
            {
                playersCheckpointData.state = CheckpointData.flagStates.Captured;
                other.GetComponent<DeathSystem>().currentCheckpoint = this.gameObject;
                other.GetComponent<PlayerData>().platformsUsed = 0;
                GetComponent<CheckpointData>().state = CheckpointData.flagStates.Current;
                PermanentDeathPlatforms();
                isTriggered = true;
            }
        }
    }
    
    private void PermanentDeathPlatforms()
    {
        GameObject[] deathPlatforms = GameObject.FindGameObjectsWithTag("DeathPlatform");
        foreach (GameObject platform in deathPlatforms) 
        {
            platform.tag = "PermanentDeathPlatform";
        }
    }
}
