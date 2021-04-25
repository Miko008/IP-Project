using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    /*public float min_x_angle,
                 max_x_angle,
                 min_z_angle,
                 max_z_angle;
    */
    public Vector3 start_angle,
                   end_angle;
    void Start()
    {
        StartCoroutine(Sway());
    }

    IEnumerator Sway()
    {
        for(;;)
        {
            //start_angle = new Vector3(Random.Range(min_x_angle,max_x_angle), 0, Random.Range(min_z_angle,max_z_angle));
            //end_angle = new Vector3(-Random.Range(min_x_angle,max_x_angle), 0, -Random.Range(min_z_angle,max_z_angle));
            for (float t = 0; t <= 1 ; t +=0.01f)
            {

                transform.Rotate(Vector3.Lerp(start_angle, end_angle, t * t * (3 - 2 * t)));
                yield return new WaitForSeconds(0.05f);
            }
            
            for (float t = 1; t >= 0 ; t -=0.01f)
            {

                transform.Rotate(Vector3.Lerp(start_angle, end_angle, t * t * (3 - 2 * t)));
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
