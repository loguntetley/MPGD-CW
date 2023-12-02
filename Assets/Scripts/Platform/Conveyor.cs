using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float ConveyorSpeed = 5f;
    //Defines the speed at which textures move on the conveyor belt.
    float x, y, scrollX = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Move the texture on the conveyor belt to make it look like it's actually moving.
        y = y + Time.deltaTime * scrollX;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
    }

    //Transport objects when they are on a conveyor belt
    public void OnCollisionStay(Collision collision)
    {
        //Vector3 position = new Vector3(Mathf.Cos(theta) * ConveyorSpeed, Mathf.Sin(theta) * ConveyorSpeed, 0);
        //collision.transform.Translate(Vector3.forward*ConveyorSpeed * Time.deltaTime, Space.World);
        //collision.transform.Translate(transform.forward * ConveyorSpeed * Time.deltaTime, Space.World);
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Debug.Log(222);
                // 计算传送带的水平移动方向和速度
                Vector3 conveyorVelocity = transform.forward * ConveyorSpeed;
                // 保留玩家当前的垂直速度
                conveyorVelocity.y = playerRigidbody.velocity.y;
                // 更新玩家的速度
                playerRigidbody.velocity = conveyorVelocity;
            }
        }
    }
}
