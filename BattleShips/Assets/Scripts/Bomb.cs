using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion,
                    splash;

    void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<Boat>().TakeDmg();
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
