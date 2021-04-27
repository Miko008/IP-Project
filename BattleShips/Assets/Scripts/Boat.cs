using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Party {Player1, Player2, Enemy}

public class Boat : MonoBehaviour
{
    public float sway_rate,
                 x_angle,
                 z_angle;
    
    public int drowning_depth = 10000;

    private int hp;

    private Party party;

    private Vector3 start_angle,
                    end_angle;

    private GameManager gameManager;

    void Start()
    {
        hp = GetComponents<BoxCollider>().Length;
        
        StartCoroutine(Sway());

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetParty(Party p)
    {
        party = p;
    }

    public Party GetParty()
    {
        return party;
    }

    public void TakeDmg()
    {
        hp--;
        if(hp == 0)
        {
            gameManager.DeleteBoat(gameObject);
            StartCoroutine(Drown());
        }
    }

    IEnumerator Sway()
    {
        for(;;)
        {
            start_angle = new Vector3(
            (Random.value > 0.5f ? 1 : -1) * Random.Range(x_angle*2f/3f,x_angle),
            0,
            (Random.value > 0.5f ? 1 : -1) *  Random.Range(z_angle*2f/3f,z_angle));
            end_angle = -start_angle;
            //new Vector3(-Random.Range(min_x_angle,max_x_angle), 0, -Random.Range(min_z_angle,max_z_angle));
            for (float t = 0; t <= 1 ; t +=0.01f)
            {

                transform.Rotate(Vector3.Lerp(start_angle, end_angle, t * t * (3 - 2 * t)));
                yield return new WaitForSeconds(1/sway_rate);
            }
            
            for (float t = 1; t >= 0 ; t -=0.01f)
            {

                transform.Rotate(Vector3.Lerp(start_angle, end_angle, t * t * (3 - 2 * t)));
                yield return new WaitForSeconds(1/sway_rate);
            }
        }
    }

    IEnumerator Drown()
    {
        float x = Random.Range(-0.1f, 0.1f),
              z = Random.Range(-0.1f, 0.1f);
        for(int i = 0; i < drowning_depth; i++)
        {
            transform.Rotate(x,0,z);
            transform.position += Vector3.down/10;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }

}
