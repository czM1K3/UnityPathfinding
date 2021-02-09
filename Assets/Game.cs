using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    const float offset = 1.02f;

    public Material nothing, wall;

    private int[,] pole;

    GameObject[,] poleBackground;
    void Start()
    {
        int[,] newPole = {
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0,  0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1,-1,-1, 0,-1,-1 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0,-2, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
        };
        pole = newPole;

        poleBackground = new GameObject[pole.GetLength(0), pole.GetLength(1)];
        for (int i = 0; i < poleBackground.GetLength(0); i++)
        {
            for (int j = 0; j < poleBackground.GetLength(1); j++)
            {
                poleBackground[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                poleBackground[i, j].transform.position = new Vector3(
                    ((float) i * offset) - (poleBackground.GetLength(0) / 2 / offset),
                    ((float) j * offset) - (poleBackground.GetLength(1) / 2 / offset)
                    );
                poleBackground[i, j].name = i + "-" + j;
                poleBackground[i, j].GetComponent<Renderer>().material = pole[i, j] == -1 ? nothing : wall;
            }
        }
    }

    void Update()
    {
        
    }
}
