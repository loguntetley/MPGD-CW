using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableData : MonoBehaviour
{
    [HideInInspector] public Transform respawnTransform;
    [HideInInspector] public int collectablesCollected = 0, collectableTotal;
    public GameObject collectableLevel, mainLevel;
    [SerializeField] private TextMeshProUGUI reamainingCollectableText;

    private void Start()
    {
        GameObject[] allCollectable = GameObject.FindGameObjectsWithTag("Collectable");
        collectableTotal = allCollectable.Length;
    }

    public void Update()
    {
        reamainingCollectableText.text = collectablesCollected.ToString() + "/"+ collectableTotal.ToString();
    }

    public void disableAllSection()
    {
        foreach (var section in collectableLevel.GetComponent<TempleSections>().sections)
        {
            section.SetActive(false);
        }
    }
}
