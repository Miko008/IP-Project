using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject particle_template;
    public float time_min = 0.5f,
                time_max = 2f;
    public int  particle_count = 5,
                spread_vertical = 25,
                spread_horizontal = 10;
    private float timer = 0,
                wait_time = 0;
                
    void Update()
    {
        GameObject particle;
        timer += Time.deltaTime;
        if(timer>wait_time)
        {
            for (int i = 0; i < particle_count; i++)
            {
                particle = Instantiate(particle_template, this.transform.position, Quaternion.identity);
                particle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * Random.Range(spread_vertical, 2*spread_vertical)
                + Vector3.right * Random.Range(-spread_horizontal, spread_horizontal) + Vector3.forward * Random.Range(-spread_horizontal, spread_horizontal));
                particle.transform.SetParent(this.transform, true);
            }
            timer -= wait_time;
            wait_time = Random.Range(time_min, time_max);
        }
    }
}
