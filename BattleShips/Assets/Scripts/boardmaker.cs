using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardmaker : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public int tileSpacing = 10;
    public GameObject oritile;


    // Start is called before the first frame update
    void Start()
    {

        GenerateGrid();        
    }

    private void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                    Instantiate(oritile, 
                    new Vector3(col*tileSpacing, 0, row * -tileSpacing),
                    Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
