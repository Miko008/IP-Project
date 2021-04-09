using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject particle_template = null;
    public int particle_count = 50,
                spread_vertical = 10,
                spread_horizontal = 20;
    GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            for (int i = 0; i < particle_count; i++)
            {
                particle = Instantiate(particle_template, this.transform.position, Quaternion.identity);
                particle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * Random.Range(spread_vertical, 5*spread_vertical)
                + Vector3.right * Random.Range(-spread_horizontal, spread_horizontal) + Vector3.forward * Random.Range(-spread_horizontal, spread_horizontal));
            }
        }
    }
}
