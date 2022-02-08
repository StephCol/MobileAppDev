using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void tapActivated();
    void dragActivated(Vector3 destination); 
    void select_toggle(bool selected);
}
