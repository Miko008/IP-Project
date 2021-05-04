using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoatSetter : MonoBehaviour
{
    public GameObject BoardManager;
    private List<GameObject> BoatsToSet = new List<GameObject>();
    public int y_offset_set = 30;

    private List<Transform> Tiles;

    public void StartAISetter(List<GameObject> Starting_Boats)
    {
        foreach (var item in Starting_Boats)
            BoatsToSet.Add(item);
        StartCoroutine(DelayedCheck());
    }

    IEnumerator DelayedCheck() 
    {
        yield return new WaitForSeconds(0.01f);                             //wait for tiles to be generated
        
        Tiles = new List<Transform>();
        Tiles.AddRange(BoardManager.GetComponentsInChildren<Transform>());  //parent Transform is also here
        Tiles.RemoveAt(0);                                                  //delete parent
        //Debug.Log(Tiles.Length);

        foreach (GameObject boat_to_set in BoatsToSet)                      //just bruteforce place boats
        {
            GameObject boat = Instantiate(boat_to_set, new Vector3(0, 0, 0), Quaternion.identity);
            BoxCollider[] colliders = boat.GetComponents<BoxCollider>();
            boat.GetComponent<Boat>().SetParty(Party.Enemy);
            do{
                boat.transform.position = Tiles[Random.Range(0, Tiles.Count)].transform.position + Vector3.up * y_offset_set;  //randomly choose position
                boat.transform.Rotate(0, 90 * Random.Range(0,3) , 0);                                                           //and rotation
            }while(!CheckCanPlace(boat, colliders));                                                                            //validate placement
            DisableTilesBelow(boat, colliders);
        }

        for (int i = 1; i < Tiles.Count; i++){                             //disable placing for all
            Tiles[i].GetComponent<Tile>().DisablePlacing();
        }
    }

    bool CheckCanPlace(GameObject boat, Collider[] colliders)               //same as in Controller.CheckCanPlace5()
    { 
        bool placeable = true;
        
        foreach(BoxCollider item in colliders){                             //check for all colliders
            Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
            RaycastHit testing_hit;
            if (Physics.Raycast(testing_ray, out testing_hit)){             //if all boxes are clear to place
                if(!testing_hit.collider.GetComponent<Tile>().CanPlace())
                    placeable = false;
            }
            else{
                placeable = false;
            }
        }
        return placeable;
    }

    void DisableTilesBelow(GameObject boat, BoxCollider[] colliders) 
    {
        foreach (BoxCollider item in colliders){                            //disable for all colliders
            Ray testing_ray = new Ray(boat.transform.position + boat.transform.rotation * Vector3.Scale(item.center, boat.transform.localScale), Vector3.down*100);
            RaycastHit testing_hit;
            if (Physics.Raycast(testing_ray, out testing_hit)) 
                testing_hit.collider.GetComponent<Tile>().DisablePlacing();
        }
    }

}
