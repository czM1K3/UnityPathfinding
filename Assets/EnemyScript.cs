using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Queue<Vector2> NextMoves;
    public Game game;
    public PlayerScript playerScript;

    void Start()
    {
        NextMoves = new Queue<Vector2>();
        InvokeRepeating("Move", 0, 0.0018f);
    }

    void Move()
    {
        Vlna((int[,])game.pole.Clone(), playerScript.GetPos());
        if (NextMoves.Count > 0)
        {
            if (NextMoves.Peek().x > transform.position.y) GoTo(Vector3.up);
            else if (NextMoves.Peek().x < transform.position.y) GoTo(Vector3.down);

            if (NextMoves.Peek().y > transform.position.x) GoTo(Vector3.right);
            else if (NextMoves.Peek().y < transform.position.x) GoTo(Vector3.left);

            if (NextMoves.Peek().x == Math.Round(transform.position.y, 2) && NextMoves.Peek().y == Math.Round(transform.position.x, 2)) NextMoves.Dequeue();
        }
        else if (NextMoves.Count <= 1)
        {
            Application.Quit();
            Debug.Log("Konec");
        }
    }

    void GoTo(Vector3 way)
    {
        transform.position += way * 0.01f;
    }

    public Vector2 GetPos()
    {
        return new Vector2((float)Math.Round(transform.position.x, 2), (float)Math.Round(transform.position.y, 2));
    }

    void Vlna(int[,] pole, Vector2 pos)
    {
        int vlna = 1;
        pole[(int)pos.x, (int)pos.y] = 1;
        for (int i = 0; i < pole.GetLength(1); i++) for (int j = 0; j < pole.GetLength(0); j++) if (pole[j, i] == -2) pole[j, i] = 0;
        Vector2 myPos = GetPos();
        pole[(int)myPos.x, (int)myPos.y] = -2;
        while (GetVlna(vlna, pole))
        {
            vlna++;
            if (vlna > 1000)
            {
                NextMoves = new Queue<Vector2>();
                return;
            }
        }
        var a = GetCesta(pole, vlna);
        //a.Reverse();
        a.RemoveAt(0);
        NextMoves = new Queue<Vector2>(a);
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
