using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion,
                    splash;
                    
    private GameManager GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        GM.NextBomb();
        collider.GetComponent<Boat>().TakeDmg();
        //Debug.Log("hit");
        Instantiate(explosion, this.transform.position,
         Quaternion.identity);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        GM.BombMiss();
        //Debug.Log("hit");
        Instantiate(splash, this.transform.position,
         Quaternion.identity);
        Destroy(gameObject);
    }
}
