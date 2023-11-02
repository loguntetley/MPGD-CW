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
        //Debug.Log("PlNum: "+playerData.platformsUsed);
        //Debug.Log("PlLimit"+currentCheckpoint.GetComponent<CheckpointData>().platformLimit);
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
        ResetPlayerDelay();
    }
    private void ResetPlatformData()
    {
        playerData.platformsUsed = 0;
        playerData.selectedPlatform = null;
        ResetPlayerDelay();
    }

    private void ResetPlayerDelay()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Invoke("ActivateMeshRender", 2);
    }

    private void ActivateMeshRender()
    {
        this.gameObject.transform.position = currentCheckpoint.transform.position;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        //bug fix(proximity bomb (die twice)): Since the player object is not destroyed when the player dies, the TrakcerTrap will continue to spawn trackers when the player dies within the range of the TrackerTrap. When the player's position is reset (fake resurrection), the Trakcer from the previous round will continue to track the player. Therefore all Trakcers need to be destroyed after the player respawns. And the rigid body range of TrakcerTrap has been slightly reduced, so that its rigid body range should not coincide with the checkpoint platform.
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Tracker");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    private void DestroyAllDeathPlatforms()
    {
        GameObject[] allDeathPlatforms = GameObject.FindGameObjectsWithTag("DeathPlatform");
        foreach (var platform in allDeathPlatforms)
            Destroy(platform);
    }
}
