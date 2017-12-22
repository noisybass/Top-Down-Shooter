using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale3D : Tween<Vector3>
{
    protected override void ApplyEasing(float t)
    {
        transform.localScale = begin + t * (end - 2f * begin);
    }
}
