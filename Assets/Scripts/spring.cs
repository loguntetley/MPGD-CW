using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{
    public float jumpforce = 10f;//change the height here
    public Rigidbody player;

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player")
        {// .tag or .name
            player = collision.gameObject.GetComponent<Rigidbody>();
            player.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

}
