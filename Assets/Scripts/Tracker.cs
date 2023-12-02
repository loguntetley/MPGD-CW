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
        transform.Translate((Player.transform.position - transform.position).normalized * TrackerSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        //Make sure it is destroyed only if it touches the player
        if (col.gameObject.name == "Player")
        {
            DestroyTracker();
        }
    }
    private void DestroyTracker()
    {
        Destroy(gameObject);
    }
}
