using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Game : MonoBehaviour
{
    const float offset = 1.02f;

    public Material nothing, wall, selected;
    //public GameObject clickEvent;

    private int[,] pole;

    private Vector2 target;

    public PlayerScript ps;

    GameObject[,] poleBackground;
    void Start()
    {
        int[,] newPole = {
            { 0, 0, 0, 0, 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0, 0, 0,-1, 0, 0, 0 },
            {-1,-1,-1,-1,-1,-1, 0,-1, 0,-1, 0,-1, 0,-1, 0,-1,-1,-1, 0,-1, 0 },
            { 0, 0, 0, 0, 0,-1, 0, 0, 0,-1, 0,-1, 0,-1, 0, 0, 0, 0, 0,-1, 0 },
            { 0,-1, 0,-1, 0,-1,-1,-1,-1,-1, 0,-1, 0,-1,-1,-1,-1,-1,-1,-1, 0 },
            { 0,-1, 0,-1, 0,-1, 0, 0, 0, 0, 0,-1, 0, 0, 0, 0, 0,-1, 0, 0, 0 },
            {-1,-1, 0,-1, 0,-1, 0,-1,-1,-1,-1,-1,-1,-1, 0,-1,-1,-1, 0,-1, 0 },
            { 0, 0, 0,-1, 0,-1, 0, 0, 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0 },
            { 0,-1,-1,-1, 0,-1,-1,-1,-1,-1, 0,-1, 0,-1,-1,-1, 0,-1,-1,-1, 0 },
            { 0,-1, 0, 0, 0, 0, 0,-1, 0,-1, 0,-1, 0,-1, 0, 0, 0,-1, 0,-1, 0 },
            { 0,-1,-1,-1,-1,-1, 0,-1, 0,-1, 0,-1,-1,-1, 0,-1,-1,-1, 0,-1, 0 },
            { 0, 0, 0, 0, 0,-1, 0, 0, 0,-1, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0 },
            { 0,-1,-1,-1, 0,-1,-1,-1,-1,-1, 0,-1, 0,-1,-1,-1,-1,-1, 0,-1, 0 },
            { 0, 0, 0,-1, 0, 0, 0, 0, 0, 0, 0,-1, 0,-1, 0, 0, 0,-1, 0,-1, 0 },
            {-1,-1, 0,-1,-1,-1,-1,-1,-1,-1,-1,-1, 0,-1, 0,-1, 0,-1, 0,-1, 0 },
            { 0, 0, 0,-1, 0, 0, 0, 0, 0, 0, 0,-1, 0,-1, 0,-1, 0, 0, 0,-1, 0 },
            { 0,-1,-1,-1,-1,-1, 0,-1,-1,-1, 0,-1, 0,-1, 0,-1,-1,-1, 0,-1, 0 },
            { 0, 0, 0, 0, 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0 },
            {-1,-1,-1,-1,-1,-1,-1,-1, 0,-1,-1,-1, 0,-1,-1,-1, 0,-1,-1,-1, 0 },
            { 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0, 0, 0,-1, 0,-1, 0,-1, 0 },
            { 0,-1,-1,-1, 0,-1, 0,-1,-1,-1, 0,-1,-1,-1,-1,-1, 0,-1, 0,-1, 0 },
            { 0, 0, 0, 0, 0,-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1, 0, 0, 0 },
        };
        pole = newPole;
        target = new Vector2(0, 0);
        pole[(int)target.x, (int)target.y] = -2;

        poleBackground = new GameObject[pole.GetLength(0), pole.GetLength(1)];
        for (int i = 0; i < poleBackground.GetLength(0); i++)
        {
            for (int j = 0; j < poleBackground.GetLength(1); j++)
            {
                poleBackground[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                poleBackground[i, j].transform.position = new Vector3((float) i, (float) j);
                poleBackground[i, j].name = i + "-" + j;
                ChangeColor(i, j, pole[i, j] == -1 ? wall : nothing);
                ClickEvent clickEvent = poleBackground[i, j].AddComponent(typeof(ClickEvent)) as ClickEvent;
                clickEvent.instructor = this;
            }
        }
    }

    public void Click(Vector2 position)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (pole[(int)position.x, (int)position.y] == 0)
            {
                ArrayChange(target, 0);
                target = position;
                ArrayChange(target, -2);
            }
        }
        else
        {
            if (pole[(int)position.x, (int)position.y] != -2)
            {
                ArrayChange(position, pole[(int)position.x, (int)position.y] == 0 ? -1 : 0);
            }
        }
        Vlna((int[,])pole.Clone(), ps.GetPos());
    }

    void ArrayChange(Vector2 position, int target)
    {
        ArrayChange((int)position.x, (int)position.y, target);
    }

    void ArrayChange(int x, int y, int target)
    {
        pole[x, y] = target;
        Material newMaterial;
        if (target == -1) newMaterial = wall;
        else if (target == -2) newMaterial = selected;
        else newMaterial = nothing;
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

    void Vlna(int[,] pole, Vector2 pos)
    {
        int vlna = 1;
        pole[(int)pos.x, (int)pos.y] = 1;
        while (GetVlna(vlna, pole))
        {
            vlna++;
            if (vlna > 1000) return;
        }
        var a = GetCesta(pole, vlna);
        a.Reverse();
        ps.NextMoves = new Queue<Vector2>(a);
    }

    bool GetVlna(int vlna, int[,] pole)
    {
        for (int i = 0; i < pole.GetLength(0); i++)
        {
            for (int j = 0; j < pole.GetLength(1); j++)
            {
                if (pole[i, j] == vlna)
                {
                    if (i - 1 >= 0 && (pole[i - 1, j] == 0 || pole[i - 1, j] == -2))
                    {
                        if (pole[i - 1, j] == -2) return false;
                        pole[i - 1, j] = vlna + 1;
                    }
                    if (i + 1 < pole.GetLength(0) && (pole[i + 1, j] == 0 || pole[i + 1, j] == -2))
                    {
                        if (pole[i + 1, j] == -2) return false;
                        pole[i + 1, j] = vlna + 1;
                    }
                    if (j - 1 >= 0 && (pole[i, j - 1] == 0 || pole[i, j - 1] == -2))
                    {
                        if (pole[i, j - 1] == -2) return false;
                        pole[i, j - 1] = vlna + 1;
                    }
                    if (j + 1 < pole.GetLength(1) && (pole[i, j + 1] == 0 || pole[i, j + 1] == -2))
                    {
                        if (pole[i, j + 1] == -2) return false;
                        pole[i, j + 1] = vlna + 1;
                    }
                }
            }
        }
        return true;
    }

    List<Vector2> GetCesta(int[,] pole, int vlna)
    {
        List<Vector2> cesta = new List<Vector2>();
        Point end = new Point();
        for (int i = 0; i < pole.GetLength(1); i++) for (int j = 0; j < pole.GetLength(0); j++) if (pole[j, i] == -2) end = new Point(i, j);
        cesta.Add(new Vector2(end.X, end.Y));
        for (int i = vlna; i >= 1; i--)
        {
            Point posledni = new Point((int)cesta[cesta.Count - 1].y, (int)cesta[cesta.Count - 1].x);
            if (posledni.X - 1 >= 0 && pole[posledni.X - 1, posledni.Y] == i)
            {
                cesta.Add(new Vector2(posledni.Y, posledni.X - 1));
            }
            else if (posledni.X + 1 < pole.GetLength(0) && pole[posledni.X + 1, posledni.Y] == i)
            {
                cesta.Add(new Vector2(posledni.Y, posledni.X + 1));
            }
            else if (posledni.Y - 1 >= 0 && pole[posledni.X, posledni.Y - 1] == i)
            {
                cesta.Add(new Vector2(posledni.Y - 1, posledni.X));
            }
            else if (posledni.Y + 1 < pole.GetLength(1) && pole[posledni.X, posledni.Y + 1] == i)
            {
                cesta.Add(new Vector2(posledni.Y + 1, posledni.X));
            }
        }
        return cesta;
    }
}
