using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This platform will vanish few seconds after the player touch it.
public class TimerPlatform : MonoBehaviour
{
    private float delay = 2f;// Change the delay time here.

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
            StartCoroutine(Destroying());
        }
    }

    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);//put animation here if it's made in the future
    }
}
