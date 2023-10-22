using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPlayer : MonoBehaviour
{
    public GameObject objectToRespawn; // 需要重新生成的物体
    public float respawnDelay = 2.0f; // 重新生成延迟时间

    private GameObject currentObject; // 当前实例化的物体

    void Start()
    {
        SpawnObject(); // 游戏开始时生成物体
    }

    void SpawnObject()
    {
        currentObject = Instantiate(objectToRespawn, transform.position, Quaternion.identity);
    }

    public void RespawnObject()
    {
        Destroy(currentObject); // 销毁当前物体
        Invoke("SpawnObject", respawnDelay); // 重新生成物体
    }
}
