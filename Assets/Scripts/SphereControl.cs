using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour, IControllable
{
    public void youveBeenTapped()
    {
        transform.position += Vector3.right;
    }

}
