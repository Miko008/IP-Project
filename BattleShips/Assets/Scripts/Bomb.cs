using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion,
                    splash;

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("hit");
        Instantiate(explosion, this.transform.position,
         Quaternion.identity);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("hit");
        Instantiate(splash, this.transform.position,
         Quaternion.identity);
        Destroy(gameObject);
    }
}
