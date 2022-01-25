using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IControllable
{
    public void youveBeenTapped()
    {
        transform.position += Vector3.down;
    }

}
