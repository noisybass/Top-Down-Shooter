using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {

    private void Awake()
    {
        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.sortingOrder = -(int)transform.position.y;
        }
    }
}
