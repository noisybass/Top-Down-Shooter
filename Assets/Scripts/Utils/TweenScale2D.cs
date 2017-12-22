using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale2D : Tween<Vector2>
{
    protected override void ApplyEasing(float t)
    {
        transform.localScale = begin + t * (end - 2f * begin);
    }
}
