using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private PlayerData playerData;

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }

    public void OnDeath()
    {
        if (playerData.platformsUsed < currentCheckpoint.GetComponent<CheckpointData>().platformLimit)
        {
            CreateDeathPlatform();
        }

        if (playerData.platformsUsed == currentCheckpoint.GetComponent<CheckpointData>().platformLimit)
        {
            DestroyAllDeathPlatforms();
            ResetPlatformData();
        }
    }

    public void OnDeath(bool wasKilled)
    {
        DestroyAllDeathPlatforms();
        ResetPlatformData();
    }
    private void CreateDeathPlatform()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Player inisiated death");
            if (playerData.selectedPlatform != null)
            {
                instantiateDeathPlatform();
                increasePlatformData();
            }
        }
    }

    private void instantiateDeathPlatform()
    {
        Vector3 deathPos = this.transform.position;
        deathPos.y -= 1;
        GameObject deathPlatform = Instantiate(playerData.selectedPlatform, deathPos, Quaternion.identity);
        deathPlatform.tag = "DeathPlatform";
    }

    private void increasePlatformData()
    {
        playerData.platformsUsed++;
        playerData.selectedPlatform = null;
        this.gameObject.transform.position = currentCheckpoint.transform.position;
    }
    private void ResetPlatformData()
    {
        playerData.platformsUsed = 0;
        playerData.selectedPlatform = null;
        this.gameObject.transform.position = currentCheckpoint.transform.position;
    }

    private void DestroyAllDeathPlatforms()
    {
        GameObject[] allDeathPlatforms = GameObject.FindGameObjectsWithTag("DeathPlatform");
        foreach (var platform in allDeathPlatforms)
            Destroy(platform);
    }
}
