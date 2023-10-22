using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed; // 移动速度
    public float jumpForce; // 跳跃力度
    public GameObject Tracker;
    Rigidbody TrackerRigi;
    public GameObject Explosion;
    public GameObject PrefabsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TrackerRigi = Tracker.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void OnMove(InputValue value)
    {
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveHorizontal * speed * Time.fixedDeltaTime, 0, moveVertical * speed * Time.fixedDeltaTime);
    }

    void OnCollisionExit(Collision col)
    {
        //Trackers are only spawned when the player leaves the trap, otherwise the player will die too quickly
        //Use Tag to obtain, there will be multiple Trackers in the scene
        if (col.gameObject.tag == "TrackerTrap")
        {
            Rigidbody p = Instantiate(TrackerRigi, col.transform.position+new Vector3(0,0.5f,0), col.transform.rotation);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Only destroys player when colliding with Trakcer
        if (col.gameObject.tag == "Tracker")
        {
            RebornPlayer();
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "SpineTrap")
        {
            RebornPlayer();
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        //After the player dies, clear all remaining Trackers
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Tracker");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    void RebornPlayer()
    {
        Instantiate(PrefabsPlayer, new Vector3(0,0.25f,-4), Quaternion.Euler(0, 0, 0));
    }

}
