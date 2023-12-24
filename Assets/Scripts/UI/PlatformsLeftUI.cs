using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformsLeftUI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int platformsLeft = player.GetComponent<DeathSystem>().currentCheckpoint.GetComponent<CheckpointData>().platformLimit - player.GetComponent<PlayerData>().platformsUsed;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Remaining: " + (platformsLeft - 1);
    }
}
