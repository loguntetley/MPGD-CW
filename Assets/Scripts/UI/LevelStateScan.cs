using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStateScan : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite active_star;
    public GameObject[] stars;
    
    void Start()
    {
        //getState(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void getState()
    {
        int score_of_star = DataController.level_score[0];
        for(int i = 0; score_of_star > 0; score_of_star--,i++)
        {
            stars[i].gameObject.GetComponent<Image>().sprite = active_star;
        }
    }
}
