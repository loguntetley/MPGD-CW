using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    private GameObject collectableData;

    void Start()
    {
        collectableData = GameObject.FindGameObjectWithTag("CollectableData");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            LoadCollectableLevel();
            collision.transform.position = GameObject.FindGameObjectWithTag("TempleSpawn").transform.position;
            gameObject.SetActive(false);
            collectableData.GetComponent<CollectableData>().collectablesCollected++;
        }
    }

    private void LoadCollectableLevel()
    {
        collectableData.GetComponent<CollectableData>().respawnTransform = transform;
        collectableData.GetComponent<CollectableData>().mainLevel.SetActive(false);
        collectableData.GetComponent<CollectableData>().collectableLevel.SetActive(true);
        collectableData.GetComponent<CollectableData>().disableAllSection();
        collectableData.GetComponent<CollectableData>().collectableLevel.GetComponent<TempleSections>().sections[collectableData.GetComponent<CollectableData>().collectablesCollected].SetActive(true);
    }
}
