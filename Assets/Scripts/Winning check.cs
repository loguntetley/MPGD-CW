using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winningcheck : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] AllCheckpoints;
    public GameObject win_text;

    // Start is called before the first frame update
    void Start()
    {
        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        CheckWinCondition();

    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        
    }

    private void CheckWinCondition()
    {
        int capturedFlags = 0;
        foreach (var checkpoint in AllCheckpoints)
        {
            if (checkpoint.GetComponent<CheckpointData>().state == CheckpointData.flagStates.Captured)
            {
                capturedFlags++;
            }
        }

        if (capturedFlags == AllCheckpoints.Length - 1)
        {
            win_text.SetActive(true);
            int unlock = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("currentScore", 3);
            PlayerPrefs.SetInt("currentLevel", unlock+1);
        }
    }
    
}
