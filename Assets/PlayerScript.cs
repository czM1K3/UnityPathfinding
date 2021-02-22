using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Queue<Vector2> NextMoves;

    void Start()
    { 
        NextMoves = new Queue<Vector2>();
        InvokeRepeating("Move", 0, 0.002f);
    }

    void Move()
    {
        if (NextMoves.Count > 0)
        {
            if (NextMoves.Peek().x > transform.position.y) GoTo(Vector3.up);
            else if (NextMoves.Peek().x < transform.position.y) GoTo(Vector3.down);

            if (NextMoves.Peek().y > transform.position.x) GoTo(Vector3.right);
            else if (NextMoves.Peek().y < transform.position.x) GoTo(Vector3.left);
            //Debug.Log(NextMoves.Peek().x + ";" + NextMoves.Peek().y);

            if (NextMoves.Peek().x == Math.Round(transform.position.y,2) && NextMoves.Peek().y == Math.Round(transform.position.x, 2)) NextMoves.Dequeue();
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
}
