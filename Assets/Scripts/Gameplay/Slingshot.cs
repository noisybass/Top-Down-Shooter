using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Slingshot : MonoBehaviour {

    [SerializeField]
    private Aim _aim;

    [SerializeField]
    private Bullet _bulletPrefab;
    private Pool<Bullet> _bulletPool;
    private bool _canShoot = true;
    private ShootEvent _shootEvent;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _bulletPool = new Pool<Bullet>(20, _bulletPrefab, transform.parent.gameObject);
        _shootEvent = new ShootEvent();
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

        // SHOOTING
        bool shoot = false;
        if (GameManager.Instance.Settings.controller)
            shoot = Input.GetAxisRaw("Shoot") == 1.0f ? true : false;
        else
            shoot = Input.GetMouseButtonDown(0);

        if (shoot)
        {
            if (_canShoot)
            {
                _canShoot = false;
                CreateBullet();
                EventManager.Instance.OnEvent(this, _shootEvent);
            }
        }
        else
            _canShoot = true;
    }

    void CreateBullet()
    {
        Bullet bullet = _bulletPool.CreateObject();
        Vector3 direction = (_aim.transform.position - transform.position).normalized;
        bullet.Init(transform.position + 2 * direction, direction, this);
    }

    public void DestroyBullet(Bullet bullet)
    {
        _bulletPool.DestroyObject(bullet);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, _aim.transform.position);
    }
}
