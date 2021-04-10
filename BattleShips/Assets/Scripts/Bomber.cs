using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject Bomb;
    public int bomb_height = 800,
                dropping_speed = 10;
    private bool has_dropped = false;
    void Update()
    {
        if (Input.GetMouseButton(0) && !has_dropped) {
			DropBomb();
            has_dropped = true;
		}
        if (Input.GetMouseButton(1))
            has_dropped=false;
    }

    void DropBomb() {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(inputRay, out hit)) {
			GameObject bomb = Instantiate(Bomb, 
            hit.collider.transform.position + bomb_height * Vector3.up, Quaternion.identity);
            bomb.GetComponent<Rigidbody>().velocity = dropping_speed * Vector3.down;
            //hit.collider.GetComponent<Renderer>().material.color = Color.green;
            if(hit.collider.GetComponent<Tile>())
                hit.collider.GetComponent<Tile>().Click();
        }
	}
}
