using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public Game instructor;

    private void OnMouseDown()
    {
        var a = this.name.Split('-');
        instructor.Click(new Vector2(Convert.ToInt32(a[0]), Convert.ToInt32(a[1])));
    }
}
