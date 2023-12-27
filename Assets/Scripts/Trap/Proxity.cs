using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proxity : MonoBehaviour
{
    public GameObject Explosion;
    public float ProxityTimer = 1;
    public Material newMaterial;
    public float explosionRadius = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Actions triggered after the player enters the range
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.Find("CFXR Fire").gameObject.SetActive(true);//��ȼ��Ч����Turn on the burning effect.
            Invoke("BoomProxity", ProxityTimer);
        }
    }

    private void BoomProxity()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        transform.Find("CFXR Fire").gameObject.SetActive(false);
        gameObject.SetActive(false);
        //Delay for one second before resetting Proxity for testing purposes. Not necessarily required in the game.
        Invoke("ResetProxity", 1);

        //Get all objects within the explosion range, and then filter out the players.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                //Call the function in the PlayerController script to destroy the player
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DeathSystem>().OnDeath(true);
            }
        }
    }

    //Reset Proxity for testing purposes
    private void ResetProxity()
    {
        gameObject.SetActive(true);
        GetComponent<Renderer>().material = newMaterial;
    }
}
