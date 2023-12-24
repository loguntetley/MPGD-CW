using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlatformDisplayer : MonoBehaviour
{
    [SerializeField] private Texture2D displayTexture = null;
    [SerializeField] private string displayName = "";
    private PlayerData playerData;
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI currentlySelectedText;

    private void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    void Update()
    {
        SetUIActive();
        GetPlatformData();
        displayPlatformData();
    }

    private void GetPlatformData()
    {
        if (playerData.selectedPlatform != null)
        {
            displayTexture = playerData.selectedPlatform.GetComponent<PlatformData>().platfromImage;
            displayName = playerData.selectedPlatform.GetComponent<PlatformData>().platformName;
        }
    }

    private void displayPlatformData()
    {
        if (playerData.selectedPlatform != null) 
        {
            image.texture = displayTexture;
            text.text = displayName;
        }
    }

    private void SetUIActive()
    {
        if (playerData.selectedPlatform != null)
        {
            image.enabled = true;
            text.enabled = true;
            currentlySelectedText.enabled = false;
        }
        if (playerData.selectedPlatform == null)
        {
            image.enabled = false;
            text.enabled = false;
            currentlySelectedText.enabled = true;
        }
    }
}
