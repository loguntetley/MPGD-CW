using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointData : MonoBehaviour
{
    public int checkpointNumber;
    public flagStates state = flagStates.Uncaptured;
    [SerializeField] private GameObject Flag;
    [SerializeField] private Material[] flagMaterials;
    public int platformLimit = 5;
    public enum flagStates
    {
        Uncaptured,
        Current,
        Captured
    }

    private void Update()
    {
        if (state == flagStates.Uncaptured)
            Flag.GetComponent<Renderer>().material = flagMaterials[0];

        if (state == flagStates.Current)
            Flag.GetComponent<Renderer>().material = flagMaterials[1];

        if (state == flagStates.Captured)
            Flag.GetComponent<Renderer>().material = flagMaterials[2];
    }
}
