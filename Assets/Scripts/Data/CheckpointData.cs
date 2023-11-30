using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointData : MonoBehaviour
{
    public int checkpointNumber;
    public flagStates state = flagStates.Uncaptured;
    //[SerializeField] private GameObject Flag;
    //[SerializeField] private Material[] flagMaterials;
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
        {
            //Flag.GetComponent<Renderer>().material = flagMaterials[0];
            //使用不同颜色的魔法光环来表示检查点的状态。
            //Use different colored magic auras to indicate checkpoint status.
            transform.Find("Magic Aura yellow").gameObject.SetActive(true);
            transform.Find("Magic Aura blue").gameObject.SetActive(false);
            transform.Find("Magic Aura green").gameObject.SetActive(false);
        }


        if (state == flagStates.Current)
        {
            //Flag.GetComponent<Renderer>().material = flagMaterials[1];
            transform.Find("Magic Aura yellow").gameObject.SetActive(false);
            transform.Find("Magic Aura blue").gameObject.SetActive(true);
            transform.Find("Magic Aura green").gameObject.SetActive(false);
        }


        if (state == flagStates.Captured)
        {
            //Flag.GetComponent<Renderer>().material = flagMaterials[2];
            transform.Find("Magic Aura yellow").gameObject.SetActive(false);
            transform.Find("Magic Aura blue").gameObject.SetActive(false);
            transform.Find("Magic Aura green").gameObject.SetActive(true);
        }

    }
}
