using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMapGenerator : MonoBehaviour
{

    //class GridSquare
    //{
    //    public GridSquare(GridSquare prev)
    //    {
    //        XPos = prev == null ? 0: prev.XPos + 1;
    //        ZPos = prev == null ? 0: prev.ZPos + 1;
    //        YPos = prev == 



    //    }

    //    public int XPos;
    //    public int YPos;
    //    public int ZPos;
    //    public List<Vector3> Points;
    //}


    //1. generera rutnät med en tom ruta mellan varje genererad ruta. höjden kan variera med vissa regler som utgångspunkt
    //2. generera övergångar mellan genererade rutor (rutor måste ansluta till minst en angränsande ruta)

    public int SquareSize = 40;

    public int GridSize = 4; //this to the power of 2 

    public GameObject protoPrefab;

    //TODO:: generera varannan på varierande höjd och gör ett diagonalt prefab som placeras på samma i höjd som angränsande ruta högst upp




    void Start()
    {
        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                Instantiate(protoPrefab, new Vector3(SquareSize * x, 0, SquareSize * z), Quaternion.identity);
            }
        }
    }

}
