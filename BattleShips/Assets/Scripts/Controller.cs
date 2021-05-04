using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //public GameObject[] Boats;
    public int y_offset = 60,
                y_offset_set = 30;
    public Material placing_mat;
    public Color shadow_color,
                 wrong_color;
    private GameObject boat;
    private Renderer rend;
    private bool is_placing = false;
    private Color ori_color;
    private Material ori_mat;
    private GameManager GM;

    //temp debug

    public bool player_enemy = true;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(is_placing)
        {
            ShowBoat();
            if(Input.GetKeyDown(KeyCode.R))
                boat.transform.Rotate(0, 90 , 0);
        }
        /*else
        {
            if(Input.GetMouseButton(0))
                PickUp();

        }*/
    }


    public void StartPlacing(GameObject _boat)
    {
        Destroy(boat);
        rend = null;

        boat = Instantiate(_boat, new Vector3(0, 0, 200), Quaternion.identity);
        rend = boat.GetComponent<Renderer>();
        
        if(player_enemy)
        {
            Debug.Log("Placing as player");
            boat.GetComponent<Boat>().SetParty(Party.Player1);
        }
        else
        {
            Debug.Log("Placing as enemy");
            boat.GetComponent<Boat>().SetParty(Party.Enemy);
        }

        ori_mat = rend.material;
        ori_color = rend.material.color;

        rend.material = placing_mat;
        rend.material.color = shadow_color;


        is_placing = true;
    }
    

    void ShowBoat() {
		Ray input_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
        //List<Component> colliders = new List<Component>();
        //GetComponents(typeof(Collider), colliders);
        BoxCollider[] colliders = boat.GetComponents<BoxCollider>();
        //Debug.Log(colliders.Length);

		if (Physics.Raycast(input_ray, out hit)) {                      //mouse -> tile
            boat.transform.position = hit.collider.transform.position + Vector3.up * y_offset;

            bool placeable = CheckCanPlace(boat, colliders);

            if(!placeable)
                rend.material.color = wrong_color;
            else
                rend.material.color = shadow_color;
            
            if(placeable && Input.GetMouseButton(0)){
                boat.transform.position = hit.collider.transform.position + Vector3.up * y_offset_set;
                //color.a = 255f;
                //boat.GetComponent<Renderer>().material.color = color;
                DisableTilesBelow(boat, colliders);

                rend.material = ori_mat;
                rend.material.color = ori_color;

                GM.ReadyForNextBoat();

                is_placing = false;
                boat = null;
        
            }
        }
    }

    bool CheckCanPlace(GameObject boat, Collider[] colliders) //also used in AIBoatSetter, could be public somewhere else but its only a uni project
    {
        bool placeable = true;
        foreach (BoxCollider item in colliders){                            //check for all colliders its still a var to make posib
            //for(int i = 0; i < colliders.Length; i++){
            //Debug.Log(colliders[i].center);
            Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
            RaycastHit testing_hit;
            //Debug.DrawRay(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100, Color.green, 1000f, false);
            if (Physics.Raycast(testing_ray, out testing_hit)){      //if all boxes are clear to place
                if(!testing_hit.collider.GetComponent<Tile>().CanPlace())
                placeable = false;
                //Debug.Log("Tiles occupied");
            }
            else{
                placeable = false;
                //Debug.Log("Tiles not hit");
            }
        }

        return placeable;
    }

    void DisableTilesBelow(GameObject boat, BoxCollider[] colliders)
    {
        foreach (BoxCollider item in colliders){       //disable for all colliders
            Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
            RaycastHit testing_hit;
            if (Physics.Raycast(testing_ray, out testing_hit)) 
                testing_hit.collider.GetComponent<Tile>().DisablePlacing();
        }
    }
}
