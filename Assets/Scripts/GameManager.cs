using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] AllCheckpoints;


    // Start is called before the first frame update
    void Start()
    {
        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCheckpointColours()
    {
        foreach (var flag in AllCheckpoints) 
        {
            if (true)
            {

            }
        }
    }
}
