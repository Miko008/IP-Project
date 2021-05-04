using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject[] Boats,
                        BoardManagers;
    public Camera       player_cam;
    public  List<GameObject> Starting_boats;
    private List<GameObject> Player_boats,
                             Enemy_boats;
    private Controller controller;
    private Bomber bomber;
    private AIBomber AIbomber;
    bool bombing = false,
         place_next_boat = true;
    Party present_turn;
    void Start()
    {
        foreach (var item in BoardManagers)
            item.GetComponent<BoardManager>().GenerateGrid();

        GetComponent<AIBoatSetter>().StartAISetter(Starting_boats);
        
        AIbomber = GetComponent<AIBomber>();
        AIbomber .StartAIBomber();

        controller = player_cam.GetComponent<Controller>();
        bomber = player_cam.GetComponent<Bomber>();
        controller.enabled = true;
        bomber.enabled = false;

        present_turn = Party.Player1;

        Player_boats = new List<GameObject>();
        Enemy_boats  = new List<GameObject>();
    }

    void GetActiveBoats()
    {
        GameObject[] temp = SceneManager.GetSceneByName("ModelsTest").GetRootGameObjects();

        foreach (GameObject x in temp)
        {
            if(x.GetComponent<Boat>()!=null)
            {
                //Debug.Log(x);
                Boat very_temp = x.GetComponent<Boat>();
                if(very_temp.GetParty() == Party.Player1)
                    Player_boats.Add(x);
                else
                    Enemy_boats.Add(x);
            }
        }

        //Debug.Log(Player_boats);
    }

    public void DeleteBoat(GameObject dead_boat)
    {
        Player_boats.Remove(dead_boat);
        Enemy_boats.Remove(dead_boat);
    }

    public void ReadyForNextBoat()
    {
        place_next_boat = true;
    }

    public void NextBomb()
    {
        if(present_turn == Party.Player1)
        {
            bomber.NextBomb();
        }
        else
        {
            Debug.Log("AI dropping");
            StartCoroutine(DelayedAIDrop());
        }
    }
    
    public void BombMiss()
    {
        if(present_turn == Party.Player1)
        {
            present_turn = Party.Enemy;
            NextBomb();
        }
        else
        {
            present_turn = Party.Player1;
            NextBomb();
        }
    }

    void Update()
    {
        if(!bombing && place_next_boat)
        {
            if(Starting_boats.Count > 0)
            {    
                controller.StartPlacing(Starting_boats[0]);
                Starting_boats.RemoveAt(0);
                place_next_boat = false;
            }
            else
            {
                GetActiveBoats();
                StartCoroutine(ChceckBoatLists());
                controller.enabled = !controller.enabled;
                bomber.enabled = !bomber.enabled;
                bombing = !bombing;
            }
            /*if(Input.GetKeyDown(KeyCode.Alpha1))
                controller.StartPlacing(Boats[0]);
            if(Input.GetKeyDown(KeyCode.Alpha2))
                controller.StartPlacing(Boats[1]);
            if(Input.GetKeyDown(KeyCode.Alpha3))
                controller.StartPlacing(Boats[2]);
            if(Input.GetKeyDown(KeyCode.Alpha4))
                controller.StartPlacing(Boats[3]);
            if(Input.GetKeyDown(KeyCode.Alpha5))
                controller.StartPlacing(Boats[4]);
            */
        }


        /*if(Input.GetKeyDown(KeyCode.Alpha0))
            controller.player_enemy = !controller.player_enemy;
        */
    }

    
    IEnumerator DelayedAIDrop()
    {
        yield return new WaitForSeconds(0.5f);

        AIbomber.DropBomb();
    }

    IEnumerator ChceckBoatLists()
    {
        for(;;)
        {
            Debug.Log("Player: " + Player_boats.Count + "\nEnemy:  " + Enemy_boats.Count);

            if(Player_boats.Count == 0)
            {
                Debug.Log("Player Loses");
            }
            if(Enemy_boats.Count  == 0)
            {
                Debug.Log("Player Wins");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
