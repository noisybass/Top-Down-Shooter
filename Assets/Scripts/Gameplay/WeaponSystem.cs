using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour {

    private Aim _aim;
    private Slingshot _slingshot;

    [SerializeField]
    private Bullet _bulletPrefab;
    private Pool<Bullet> _bulletPool;
    private bool _canShoot = true;
    private ShootEvent _shootEvent;

    private void Awake()
    {
        _aim = GetComponentInChildren<Aim>();
        _slingshot = GetComponentInChildren<Slingshot>();

        _bulletPool = new Pool<Bullet>(20, _bulletPrefab, gameObject);
        _shootEvent = new ShootEvent();
    }

    public void CustomUpdate(int playerSortingOrder)
    {
        _aim.CustomUpdate();
        _slingshot.CustomUpdate(playerSortingOrder + 1);


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
                _slingshot.Kick();
                EventManager.Instance.OnEvent(this, _shootEvent);
            }
        }
        else
            _canShoot = true;
    }

    public Vector3 AimPosition()
    {
        return _aim.transform.position;
    }

    void CreateBullet()
    {
        Bullet bullet = _bulletPool.CreateObject();
        Vector3 direction = (_aim.transform.position - transform.position).normalized;
        bullet.Init(transform.position + 2 * direction, direction);
    }

    public void DestroyBullet(Bullet bullet)
    {
        _bulletPool.DestroyObject(bullet);
    }

    private void OnDrawGizmos()
    {
        if (_aim != null)
            Gizmos.DrawLine(transform.position, _aim.transform.position);
    }
}
