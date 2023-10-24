using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerController.readytoJump);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //Debug.Log("jumpout!!!");
            PlayerController.readytoJump = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //Debug.Log("in jump");
            PlayerController.readytoJump = true;
        }
    }


}
