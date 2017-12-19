using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition2D : Tween<Vector2>
{
    protected override void ApplyEasing(float t)
    {
        transform.position = _begin + t * (_end - 2f * _begin);
    }
}
