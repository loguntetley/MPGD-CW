using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public float TrackerSpeed;
    public float TrackerTimer;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Invoke("DestroyTracker", TrackerTimer);
    }

    // Update is called once per frame
    void Update()
    {
        //Tracker tracks players
        transform.Translate((Player.transform.position - transform.position).normalized * TrackerSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //Make sure it is destroyed only if it touches the player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<DeathSystem>().OnDeath(true);
            DestroyTracker();
        }
    }
    void DestroyTracker()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        //After the player dies, clear all remaining Trackers
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Tracker");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

}
