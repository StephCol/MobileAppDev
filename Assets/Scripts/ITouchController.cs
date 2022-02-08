using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchController
{
    void tap(Vector2 position);

    void pinch(Vector2 current_position);

    void drag(Vector2 position1, Vector2 position2, float relative_distance);

}
