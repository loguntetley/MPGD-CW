using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleDeathWall : MonoBehaviour
{

    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = spawnPoint.transform.position;
        }
    }
}
