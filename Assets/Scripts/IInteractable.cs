using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void tapActivated();
    void dragActivated(Ray our_ray, float destination); 
    void select_toggle(bool selected);
}
