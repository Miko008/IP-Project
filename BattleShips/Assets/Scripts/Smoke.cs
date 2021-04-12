using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject particle_template;
    public float spawn_time_min = 0.5f,
                 spawn_time_max = 2f,
                 fade_time_min = 10f,
                 fade_time_max = 20f,
                 color_hue_min = 70f,
                 color_hue_max = 255f,
                 scale_min = .5f,
                 scale_max = 2f,
                 scale_change = 0.2f;
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
                float temp_scale = Random.Range(scale_min,scale_max);
                particle.GetComponent<SmokeParticle>().SetUp(Random.Range(color_hue_min,color_hue_max),Random.Range(fade_time_min,fade_time_max), new Vector3(temp_scale,temp_scale,temp_scale), scale_change);
                particle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * Random.Range(spread_vertical, 2*spread_vertical)
                + Vector3.right * Random.Range(-spread_horizontal, spread_horizontal) + Vector3.forward * Random.Range(-spread_horizontal, spread_horizontal));
                particle.transform.SetParent(this.transform, true);
            }
            timer -= wait_time;
            wait_time = Random.Range(spawn_time_min, spawn_time_max);
        }
    }
}
