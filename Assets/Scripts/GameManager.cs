using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] AllCheckpoints;
    public int scenceBuildValue;
    //public ScriptDontDestroy leveldata;
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
            //int New_level = PlayerPrefs.GetInt("currentLevel");
            //if (unlock == New_level)
            //{
             ScriptDontDestroy._scriptDontDestroy.levels[unlock-1] = 3;
                //PlayerPrefs.SetInt("currentScore", 3);
                //PlayerPrefs.SetInt("currentLevel", unlock + 1);
            //}
        }
    }
   
}
