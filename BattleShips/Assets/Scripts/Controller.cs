using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //public GameObject[] Boats;
    public int y_offset = 60,
                y_offset_set = 30;
    private GameObject boat;
    private bool is_placing = false;
    private Color color;


    void Update()
    {
        if (is_placing) {
            ShowBoat();
            if(Input.GetKeyDown(KeyCode.R))
                boat.transform.Rotate(0, 90 , 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && !is_placing) {
            //boat = Instantiate(Boats[0], new Vector3(0, 0, 200), Quaternion.identity);
            //color = boat.GetComponent<Renderer>().material.color;
            //color.a = 120f;
            //boat.GetComponent<Renderer>().material.color = color;
            is_placing = true;
        }
    }

    public void StartPlacing(GameObject _boat)
    {
        Destroy(boat);
        boat = Instantiate(_boat, new Vector3(0, 0, 200), Quaternion.identity);
            //color = boat.GetComponent<Renderer>().material.color;
            //color.a = 120f;
            //boat.GetComponent<Renderer>().material.color = color;
            is_placing = true;
    }
    

    void ShowBoat() {
		Ray input_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
        bool placeable = true;
        //List<Component> colliders = new List<Component>();
        //GetComponents(typeof(Collider), colliders);
        BoxCollider[] colliders = boat.GetComponents<BoxCollider>();
        
        //Debug.Log(colliders.Length);


		if (Physics.Raycast(input_ray, out hit)) {                      //mouse -> tile
            placeable = true;
            boat.transform.position = hit.collider.transform.position + Vector3.up * y_offset;
            //Debug.Log("asdadas");
            foreach (var item in colliders){       //check for all colliders
            //for(int i = 0; i < colliders.Length; i++){
                //Debug.Log(colliders[i].center);
                Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
                RaycastHit testing_hit;
                //Debug.DrawRay(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100, Color.green, 1000f, false);
                if (Physics.Raycast(testing_ray, out testing_hit)){      //if all boxes are clear to place
                    if(!testing_hit.collider.GetComponent<Tile>().CanPlace())
                    {
                        placeable = false;
                        //Debug.Log("nie mozna postawic");
                    }
                }
                else
                {
                    //Debug.Log("nie trafiono");
                    placeable = false;
                }
            }
            if(placeable && Input.GetMouseButton(0)){
                boat.transform.position = hit.collider.transform.position + Vector3.up * y_offset_set;
                //color.a = 255f;
                //boat.GetComponent<Renderer>().material.color = color;
                foreach (var item in colliders){       //disable for all colliders
                    Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
                    RaycastHit testing_hit;
                        if (Physics.Raycast(testing_ray, out testing_hit)) 
                            testing_hit.collider.GetComponent<Tile>().DisablePlacing();
                }
                hit.collider.GetComponent<Tile>().DisablePlacing();
                is_placing = false;
                boat = null;
        
            }
        }
    }
}
