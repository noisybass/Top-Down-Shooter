using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Slingshot : MonoBehaviour {

    [SerializeField]
    private Aim _aim;
    private SpriteRenderer _renderer;

    private bool _kicking = false;

    private void Awake()
    {
        _renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    public void CustomUpdate (int sortingOrder)
    {
        // UPDATE GRAPHICS
        Vector2 slingshotToAim = _aim.transform.position - transform.position;
        float angle = Mathf.Atan2(slingshotToAim.y, slingshotToAim.x) * Mathf.Rad2Deg;        

        Vector3 newScale = Vector3.one;
        if (angle >= 90 && angle <= 270 || angle <= -90 && angle >= -270)
        {
            newScale.x = -1.0f;
            angle += 180.0f;
        }

        transform.localScale = newScale;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        _renderer.sortingOrder = sortingOrder;
    }

    public void Kick()
    {
        if (!_kicking)
        {
            _kicking = true;
            StartCoroutine(KickCoroutine());
        }
    }

    IEnumerator KickCoroutine()
    {
        Vector3 oldPos = _renderer.gameObject.transform.localPosition;
        _renderer.gameObject.transform.localPosition += new Vector3(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.2f);
        _renderer.gameObject.transform.localPosition = oldPos;
        _kicking = false;
    }
}
