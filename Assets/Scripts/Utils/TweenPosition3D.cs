using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition3D : Tween<Vector3>
{
    protected override void ApplyEasing(float t)
    {
        transform.position = begin + t * (end - 2f * begin);
    }
}
