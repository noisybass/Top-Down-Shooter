using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    [SerializeField]
    private Aim _aim;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    public void CustomUpdate () {
        Vector2 slingshotToAim = _aim.transform.position - transform.position;
        float angle = Mathf.Atan2(slingshotToAim.y, slingshotToAim.x) * Mathf.Rad2Deg;        

        Vector3 newScale = Vector3.one;
        if (angle >= 90 && angle <= 270 || angle <= -90 && angle >= -270)
        {
            newScale.x = -1.0f;
            angle += 180.0f;
        }
        else
        {
            newScale.x = 1.0f;
        }
        transform.localScale = newScale;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        _renderer.sortingOrder = -(int)transform.position.y + 1;
    }
}
