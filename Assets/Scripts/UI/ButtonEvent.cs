using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public GameObject levelselection;
    public GameObject settingmenu;
    private GameObject currentButton;
    public GameObject changing_state_UI;
    public GameObject StartMenu;
    public GameObject PauseMenu;
    ///////music part/////////
    private AudioSource musicAudio;
    public AudioClip buttonClip;
    public AudioSource gameAudio;
    // Start is called before the first frame update
    void Start()
    {

        musicAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*___________________________button_______________________________*/
    public void On_start_button()
    {
        StartMenu.SetActive(false);
        levelselection.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);

    }
    public void On_setting_button()
    {
        StartMenu.SetActive(false);
        settingmenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);
    }
    public void On_selection_button(int j)
    {
        /*currentButton = EventSystem.current.currentSelectedGameObject;
        string s = currentButton.GetComponent<TextMeshPro>().text;
        int i = 0;
        int.TryParse(s, out i);//Change the value tpye into int*/
        gameAudio.PlayOneShot(buttonClip);
        try
        {
            SceneManager.LoadScene(j);
        }
        catch 
        {
            Debug.Log("No such level select");
        }
    }
    public void On_levelExit_button()
    {
        levelselection.SetActive(false);
        StartMenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);

    }
    public void On_settingExit_button()
    {
        settingmenu.SetActive(false);
        StartMenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);
    }
    public void OnExitGame()
    {
        gameAudio.PlayOneShot(buttonClip);
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void On_controller_button(string s)
    {
        gameAudio.PlayOneShot(buttonClip);

        changing_state_UI.gameObject.SetActive(true);
        if(Input.anyKey)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                changing_state_UI.gameObject.SetActive(false);
            }
            else
            {
                
            }
        }
    }
    public void On_Music_Value_Change(Slider slider)
    {
        //Debug.Log(slider.value);
        musicAudio.volume = slider.value;
    }
    public void On_Game_Value_Change(Slider slider)
    {
        gameAudio.volume = slider.value;
    }
    /// <summary>
    /// game playing button
    /// </summary>
    public void On_pause_button()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void On_Resume_button()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void On_Restart_button(int i)
    {
        Time.timeScale = 1;
        Score_check();
        SceneManager.LoadScene(i);
    }
    public void On_Menu_button()
    {
        Time.timeScale = 1;
        Score_check();
        SceneManager.LoadScene(0);
    }
    public void On_Next_level(int i)
    {
        Time.timeScale = 1;
        Score_check();
        SceneManager.LoadScene(i);
    }
    private void Score_check()
    {
        GameObject[] AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        int capturedFlags = 0;
        foreach (var checkpoint in AllCheckpoints)
        {
            if (checkpoint.GetComponent<CheckpointData>().state == CheckpointData.flagStates.Captured)
            {
                capturedFlags++;
            }
        }
        int i = capturedFlags / AllCheckpoints.Length * 3;
        PlayerPrefs.SetInt("currentScore", i);
    }
}
