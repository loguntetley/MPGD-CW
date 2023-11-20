using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Levels_UnlockLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    public GameObject buttonPoint;
    private GameObject[] button_objects;
    public Sprite active_star;
    public Sprite active_level;
    void Start()
    {
        int unlock = PlayerPrefs.GetInt("currentLevel");
        //button_objects = new GameObject[buttonPoint.transform.childCount];
        buttons = new Button[buttonPoint.transform.childCount];
        for (int i = 0; i < buttonPoint.transform.childCount; i++)
        {
            buttons[i] = buttonPoint.transform.GetChild(i).GetComponent<Button>();
        }
        //for (int i = 0; i < buttonPoint.transform.childCount; i++)
        {
            //button_objects[i] = buttonPoint.transform.GetChild(i).GetComponent<GameObject>();
        }
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlock; i++)
        {
            buttons[i].interactable = true;
            buttons[i].GetComponent<Image>().sprite = active_level;

        }
        getState(unlock - 1);

    }
    
    void getState(int num)
    {
        //active gameobjects
        GameObject[] objects = new GameObject[4];
        //GameObject[] objects = buttons[num].GetComponentsInChildren<GameObject>(false);
        objects[0] = buttons[num].transform.Find("Text(TMP)").gameObject;
        objects[1] = buttons[num].transform.Find("star_1").gameObject;
        objects[2] = buttons[num].transform.Find("star_2").gameObject;
        objects[3] = buttons[num].transform.Find("star_3").gameObject;
        foreach (var star in objects)
        {
            star.SetActive(true);
        }
        //active the stars
        int score_of_star = PlayerPrefs.GetInt("currentScore");
        Image[] stars = buttons[num].GetComponentsInChildren<Image>();
        //int score_of_star = DataController.level_score[0];
        for (int i = 1; score_of_star > 0; score_of_star--,i++)
        {
            stars[i].sprite = active_star;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
