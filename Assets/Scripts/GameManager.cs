using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] AllCheckpoints;
    public int scenceBuildValue;

    // Start is called before the first frame update
    void Start()
    {
        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition(scenceBuildValue);
    }

    private void CheckWinCondition(int scenceBuildValue)
    {
        int capturedFlags = 0;
        foreach (var checkpoint in AllCheckpoints)
        {
            if (checkpoint.GetComponent<CheckpointData>().state == CheckpointData.flagStates.Captured)
            {
                capturedFlags++;
            }
        }

        if (capturedFlags  == AllCheckpoints.Length - 1) 
        {
            SceneManager.LoadScene(scenceBuildValue);
        }
    }
}
