using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition3D : Tween<Vector3>
{
    protected override void ApplyEasing(float t)
    {
        transform.position = _begin + t * (_end - 2f * _begin);
    }
}
