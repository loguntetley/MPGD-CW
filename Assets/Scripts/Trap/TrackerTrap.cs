using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerTrap : MonoBehaviour
{
    private bool isTriggered = false;
    [SerializeField] private GameObject tracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered)
        {
            Instantiate(tracker, new Vector3(this.transform.position.x, this.transform.position.y + 0.8f, this.transform.position.z), Quaternion.identity);
            Invoke("TriggerResest", 3);
            isTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isTriggered)
        {
            Instantiate(tracker, new Vector3(this.transform.position.x, this.transform.position.y+ 0.8f, this.transform.position.z), Quaternion.identity);
            Invoke("TriggerResest", 3);
            isTriggered = true;
        }
    }
    private void TriggerResest()
    {
        isTriggered = false;
    }
}
