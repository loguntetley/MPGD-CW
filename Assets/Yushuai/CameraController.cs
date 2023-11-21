using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z + 4) ;
    }

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + offset;
    }
}