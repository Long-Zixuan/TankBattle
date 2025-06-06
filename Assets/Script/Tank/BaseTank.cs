using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTank : MonoBehaviour
{
    public Animator _animator;
    
    public GameObject bulletPrefab;
    protected Rigidbody _rb;

    protected bool _isDie = false;

    protected bool _dieed = false;

    protected Transform _tankGun;
    // 坦克移动速度
    public float speed = 2;

    // 坦克转身速度
    public float turnSpeed = 5;
    // 发射炮弹初速度
    public float fireSpeed = 40;
    public string desc = "";
    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _tankGun = transform.Find("TankGun");
        if (bulletPrefab == null)
        {
            bulletPrefab = GameObject.Find("Bullet");
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_isDie)
        {
            DieLogic();
            return;
        }
        MoveLogic();
    }


    protected virtual void DieLogic()
    {
        if (_dieed)
        {
            return;
        }
        _animator.SetTrigger("Die");
        _dieed = true;
    }

    protected virtual void MoveLogic()
    {
        
    }
    

}
