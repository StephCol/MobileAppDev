using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControllable
{
    void youveBeenTapped();
    void MoveTo(Vector3 destination);
    void Update();
}
