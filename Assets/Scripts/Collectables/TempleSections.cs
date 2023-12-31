using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleSections : MonoBehaviour
{
    public GameObject[] sections;

    private void OnDisable()
    {
        foreach (var section in sections) 
        {
            section.SetActive(false);
        }
    }
}
