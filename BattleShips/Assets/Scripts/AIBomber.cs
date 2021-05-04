using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBomber : MonoBehaviour
{
    public int bomb_height = 800,
               dropping_speed = 10;
    public GameObject BoardManager,
                      Bomb;
    private List<Transform> Tiles;


    public void StartAIBomber()
    {
        StartCoroutine(DelayedCheck());
    }

    IEnumerator DelayedCheck() 
    {
        yield return new WaitForSeconds(0.01f);                             //wait for tiles to be generated
        Tiles = new List<Transform>();
        Tiles.AddRange(BoardManager.GetComponentsInChildren<Transform>());  //parent Transform is also here
        Tiles.RemoveAt(0);                                                  //delete parent
    }

    public void DropBomb()
    {
        int temp = Random.Range(0, Tiles.Count);
        GameObject bomb = Instantiate(Bomb, 
                    Tiles[temp].transform.position + bomb_height * Vector3.up, Quaternion.identity);
                    bomb.GetComponent<Rigidbody>().velocity = dropping_speed * Vector3.down;
        Tiles[temp].GetComponent<Tile>().Click();
        Tiles.RemoveAt(temp);
    }
}
