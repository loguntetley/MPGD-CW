using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentPlatform : MonoBehaviour
{
    private float visibilityThreshold = 25f; // 玩家与平台的距离阈值
    private float fadeSpeed = 3f; // 平台渐变的速度
    private Material platformMaterial;
    private Color originalColor;
    private Transform playerTransform;

    private void Start()
    {
        platformMaterial = GetComponent<Renderer>().material;
        originalColor = platformMaterial.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 假设玩家标签为"Player"
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        float alpha = Mathf.Clamp01(1 - (distance / visibilityThreshold));

        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        platformMaterial.color = Color.Lerp(platformMaterial.color, newColor, fadeSpeed * Time.deltaTime);
    }
}
