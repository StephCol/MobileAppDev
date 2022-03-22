using UnityEngine;

public interface IInteractable 
{
    void dragActivated(Ray our_ray, float destination);
    void select_toggle(bool selected);

}
