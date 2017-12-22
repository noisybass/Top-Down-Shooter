using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition2D : Tween<Vector2>
{
    protected override void ApplyEasing(float t)
    {
        transform.position = begin + t * (end - 2f * begin);
    }
}
