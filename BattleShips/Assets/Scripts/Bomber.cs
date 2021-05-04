using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject Bomb,
                      Beam;
    public int  beam_height = 0,
                fade_time = 10,
                bomb_height = 800,
                dropping_speed = 10;
    private bool has_dropped = false;
    private int last_tile;
    private GameObject temp;

    void Update()
    {
        Show();
        //if (Input.GetMouseButton(1))
        //    has_dropped=false;
    }


    void Show() {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(inputRay, out hit)) {
            if(hit.collider.GetComponent<Tile>() && hit.collider.GetComponent<Tile>().CanBomb()){
                
                if(hit.collider.GetInstanceID()!=last_tile){
                    if(temp != null)
                        StartCoroutine(DeleteBeam(temp));
                    temp = Instantiate(Beam, hit.collider.transform.position + beam_height * Vector3.up, Quaternion.identity);
                    last_tile = hit.collider.GetInstanceID();
                    StartCoroutine(MoreDelay(temp));
                }
                
                if(Input.GetMouseButton(0) && !has_dropped){
                    has_dropped = true;
                    GameObject bomb = Instantiate(Bomb, 
                    hit.collider.transform.position + bomb_height * Vector3.up, Quaternion.identity);
                    bomb.GetComponent<Rigidbody>().velocity = dropping_speed * Vector3.down;
                    hit.collider.GetComponent<Tile>().Click();
                }
            }    
        }
	}

    public void NextBomb()
    {
        has_dropped = false;
    }

    IEnumerator MoreDelay(GameObject Beam)
    {
        for(int i = 0; i < 3; i++)
            yield return new WaitForSeconds(1f);
        
        if(Beam != null)
            StartCoroutine(DeleteBeam(Beam));
    }

    IEnumerator DeleteBeam(GameObject Beam)
    {
        Renderer rend = Beam.GetComponent<Renderer>();
        Color c = rend.material.color;
        for(float i = 0; i <= 1; i += 0.05f)
        {
            if(Beam == null)
                yield break;
            c.a = 1f-i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        
        Destroy(Beam);
    }
}
