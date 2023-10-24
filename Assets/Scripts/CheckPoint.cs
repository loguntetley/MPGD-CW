using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



    public class CheckPoint : MonoBehaviour
    {
        public GameObject Player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.GetComponent<PlayerController>().currentCheckpoint = this; 
                this.gameObject.SetActive(false);
                Invoke("Check_point_awake", 2.0f);
            }
        }
        public void Check_point_awake()
        {
            this.gameObject.SetActive(true);
        }
    }

    

