using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    const float offset = 1.02f;

    public Material nothing, wall, selected;
    //public GameObject clickEvent;

    private int[,] pole;

    private Vector2 target;

    GameObject[,] poleBackground;
    void Start()
    {
        int[,] newPole = {
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0,  0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1,-1,-1, 0,-1,-1 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0 },
        };
        pole = newPole;
        target = new Vector2(7, 8);
        pole[(int)target.x, (int)target.y] = -2;

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
                ChangeColor(i, j, pole[i, j] == -1 ? nothing : wall);
                ClickEvent clickEvent = poleBackground[i, j].AddComponent(typeof(ClickEvent)) as ClickEvent;
                clickEvent.instructor = this;
            }
        }
    }

    void Update()
    {
        
    }

    public void Click(Vector2 position)
    {
        //Debug.Log("Click " + position.x + " - " + position.y);
        if (pole[(int)position.x,(int)position.y] == 0)
        {
            ArrayChange(target, 0);
            //ChangeColor(target, wall);
            target = position;
            ArrayChange(target, -2);
            //ChangeColor(target, selected);
        }
        

        
    }

    void ArrayChange(Vector2 position, int target)
    {
        ArrayChange((int)position.x, (int)position.y, target);
    }

    void ArrayChange(int x, int y, int target)
    {
        pole[x, y] = target;
        Material newMaterial;
        if (target == -1) newMaterial = nothing;
        else if (target == -2) newMaterial = selected;
        else newMaterial = wall;
        ChangeColor(x, y, newMaterial);
    }

    void ChangeColor(Vector2 position, Material newMaterial)
    {
        ChangeColor((int)position.x, (int)position.y, newMaterial);
    }

    void ChangeColor(int x, int y, Material newMaterial)
    {
        poleBackground[x, y].GetComponent<Renderer>().material = newMaterial;
    }
}
