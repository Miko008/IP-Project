using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject oritile;
    public int rows = 10,
                cols = 10,
                tileSpacing = 40;
                


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();        
    }

    private void GenerateGrid()
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
            }
        }
    }
}
