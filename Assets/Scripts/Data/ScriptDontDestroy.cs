using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDontDestroy: MonoBehaviour
{
    // Start is called before the first frame update
    public static ScriptDontDestroy _scriptDontDestroy;
    public int[] levels = new int[10];
    public float Music_Value = 0.5f;
    public float game_sound_Value = 0.5f;
    private void Awake()
    {
        Music_Value = 0.5f;
        game_sound_Value = 0.5f;
     }
    void Start()
    {
        if (_scriptDontDestroy == null)
        {
            _scriptDontDestroy = this;
            DontDestroyOnLoad(_scriptDontDestroy);
            Debug.Log("´´½¨");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
