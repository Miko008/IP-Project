using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject oritile;
    public bool is_player1 = false;
    public int rows = 10,
                cols = 10,
                tileSpacing = 40;

    public void GenerateGrid()
    {
        GameObject temp_tile;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                    temp_tile = Instantiate(oritile, 
                    this.transform.position + new Vector3(col*tileSpacing, 0, row * -tileSpacing),
                    Quaternion.identity);
                    temp_tile.transform.SetParent(this.transform, true);
                    if(!is_player1)
                        temp_tile.GetComponent<Tile>().EnableBombing();
            }
        }
    }
}
