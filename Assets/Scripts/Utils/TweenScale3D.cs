using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale3D : Tween<Vector3>
{
    protected override void ApplyEasing(float t)
    {
        transform.localScale = _begin + t * (_end - 2f * _begin);
    }
}
