using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IControllable
{
    private Vector3 drag_position;

    public void MoveTo(Vector3 destination)
    {
        transform.position = destination;
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

    public void youveBeenTapped()
    {
        transform.position += Vector3.down;
    }

}
