using UnityEngine;

public class DropFloor : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool hasBeenTriggered = false;

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log(this.name +" "+ hasBeenTriggered);
        if (other.gameObject.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            Invoke("DropTile", 0.2f); // 延迟0.2秒后掉落
        }
    }

    void DropTile()
    {
        // 添加Rigidbody组件使方格掉落
        //gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        Invoke("destroyDropFloor", 3f);//掉落的地板3秒后自动销毁
        Invoke("resetDropFloor", 5f);//掉落的地板5秒后重置
    }
    void destroyDropFloor()
    {
        gameObject.SetActive(false);
    }
    void resetDropFloor()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        hasBeenTriggered = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
    }
}
