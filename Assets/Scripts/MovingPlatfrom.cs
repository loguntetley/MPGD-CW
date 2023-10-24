using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    public float counter = 0; 
    public float incrementPerFrame = 1.0f; 
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        counter += Time.deltaTime * incrementPerFrame;
        int t = (int)counter;
        if (t % 2 == 0){
            rb.velocity = new Vector3(0,0,5);//other effect is ok too,except SetActive()
        }else{
            rb.velocity = new Vector3(0,0,-5);
        }
    }

}