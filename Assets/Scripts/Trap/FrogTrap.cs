using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTrap : MonoBehaviour
{
    public ParticleSystem fogParticleSystem;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float visibleDuration = 5.0f;

    private ParticleSystem.MainModule fogMain;

    void Start()
    {
        fogMain = fogParticleSystem.main;
        fogParticleSystem.Stop();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FogSequence());
        }
    }

    private IEnumerator FogSequence()
    {
        fogParticleSystem.Play(); // 启动粒子系统

        // 渐入
        float timer = 0.0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeInDuration;
            SetFogAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        // 渐出
        timer = 0.0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1 - (timer / fadeOutDuration);
            SetFogAlpha(alpha);
            yield return null;
        }

        fogParticleSystem.Stop(); // 停止粒子系统
    }

    private void SetFogAlpha(float alpha)
    {
        var color = fogMain.startColor.color;
        color.a = alpha;
        fogMain.startColor = color;
    }
}
