using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour, IControllable
{
    private Vector3 drag_position;

    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
    }

    public void youveBeenTapped()
    {
        transform.position += Vector3.right;
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

}
