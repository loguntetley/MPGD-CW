using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            Invoke("DropTile", 0.15f); // 延迟1秒后掉落
        }
    }

    void DropTile()
    {
        // 添加Rigidbody组件使方格掉落
        gameObject.AddComponent<Rigidbody>();
        Invoke("destroyDropFloor", 3f);//掉落的地板3秒后自动销毁

    }
    void destroyDropFloor()
    {
        Destroy(gameObject);
    }
}
