using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject particle_template = null;
    public int  particle_count = 50,
                spread_vertical = 10,
                spread_horizontal = 20;
    public float scale_min,
                 scale_max,
                 live_max,
                 live_min,
                 scale_change;
    public Color start_color_min,
                 start_color_max,
                 end_color_min,
                 end_color_max;
                 
    GameObject particle;

    Color RandColor(Color min, Color max)
    {
        float h, s, v, H, S, V;
        Color.RGBToHSV(min, out h, out s, out v);
        Color.RGBToHSV(max, out H, out S, out V);
        return Random.ColorHSV(h,H,s,S,v,V);
    }

    void Start()
    {
        for (int i = 0; i < particle_count; i++)
        {
            particle = Instantiate(particle_template, this.transform.position, Quaternion.identity);
            
            float temp_scale = Random.Range(scale_min,scale_max);

            particle.GetComponent<ExplosionParticle>().SetUp( RandColor(start_color_min, start_color_max),  RandColor(end_color_min, end_color_max), 
            new Vector3(temp_scale,temp_scale,temp_scale), 
            Random.Range(live_min, live_max),
            Random.Range(scale_change, 2 * scale_change));
            
            particle.GetComponent<Rigidbody>().velocity = 
            transform.TransformDirection(Vector3.up * Random.Range(spread_vertical, 5*spread_vertical)
            + Vector3.right * Random.Range(-spread_horizontal, spread_horizontal) 
            + Vector3.forward * Random.Range(-spread_horizontal, spread_horizontal));

            particle.transform.SetParent(this.transform, true);
        }
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp() 
    {
        for(float timer = 0; timer < live_max*3; timer += 0.5f) 
            yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
