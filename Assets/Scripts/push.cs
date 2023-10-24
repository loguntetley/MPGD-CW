using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour
{
    public float pushforce = 10f;
    public Rigidbody player;
    public Vector3 GetRandomUnitVector()
    {
        float x = (float)(Random.value-0.5)*2;
        float y = Random.value;
        float z = (float)(Random.value-0.5)*2;
        return new Vector3(x, y, z).normalized;
    }


    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag =="Player"){ // .tag or .name
            player = collision.gameObject.GetComponent<Rigidbody>();
            player.AddForce(GetRandomUnitVector()*pushforce, ForceMode.Impulse);
        }
    }
}