using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBack : MonoBehaviour
{
    private GameObject collectableData;
    // Start is called before the first frame update
    void Start()
    {
        collectableData = GameObject.FindGameObjectWithTag("CollectableData");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            LoadMainLevel();
        }
    }

    private void LoadMainLevel()
    {
        collectableData.GetComponent<CollectableData>().mainLevel.SetActive(true);
        collectableData.GetComponent<CollectableData>().collectableLevel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.position = collectableData.GetComponent<CollectableData>().respawnTransform.position; 
    }
}

