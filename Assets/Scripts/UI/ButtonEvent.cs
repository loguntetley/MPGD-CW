using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonEvent : MonoBehaviour
{
    public GameObject levelselection;
    private GameObject currentButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void On_start_button()
    {
        levelselection.SetActive(true);
    }
    public void On_selection_button(int j)
    {
        /*currentButton = EventSystem.current.currentSelectedGameObject;
        string s = currentButton.GetComponent<TextMeshPro>().text;
        int i = 0;
        int.TryParse(s, out i);//Change the value tpye into int*/
        try
        {
            SceneManager.LoadScene(j);
        }
        catch 
        {
            Debug.Log("No such level select");
        }

            

    }
}
