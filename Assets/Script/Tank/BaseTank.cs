using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTank : MonoBehaviour
{
    public Animator _animator;
    
    public GameObject bulletPrefab;
    protected Rigidbody _rb;

    protected bool _isDie = false;

    protected AudioSource _move;
    protected AudioSource _fire;
    protected AudioSource _boom;
    
    
    public bool IsDie
    {
        get { return _isDie; }
        set { _isDie = value; }
    }

    protected int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    protected bool _isStoping = false;

    protected Transform _tankGun;
    // 坦克移动速度
    public float speed = 2;

    // 坦克转身速度
    public float turnSpeed = 5;
    // 发射炮弹初速度
    public float fireSpeed = 20;
    public string desc = "";

    public BaseSceneLogic sceneLogic;
    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _tankGun = transform.Find("TankGun");
        _move = GetComponent<AudioSource>();
        _fire = _tankGun.gameObject.GetComponent<AudioSource>();
        _boom = transform.Find("TankRenderers").GetComponent<AudioSource>();
        if (bulletPrefab == null)
        {
            bulletPrefab = GameObject.Find("Bullet");
        }
    }
    
    void PlayAudioLogic()
    {
        
        if (Vector3.Distance(_rb.velocity, Vector3.zero) < 0.01f)
        {
            _move.Stop();
        }
        else
        {
            _move.Play();
        }

    }

    // Update is called once per frame
    protected void Update()
    {
        if (_isStoping)
        {
            return;
        }
        //print(desc+":"+IsDie);
        PlayAudioLogic();
        if (_isDie)
        {
            DieLogic();
            return;
        }
        MoveLogic();
    }

    public virtual void Attacked()
    {
        print("base被攻击");
    }
    


    protected virtual void DieLogic()
    {
        print("base死亡");
        try
        {
            _animator.SetTrigger("Die");
            _boom.Play();
        }
        catch (System.Exception e)
        {
            print(e.ToString());
        }
        

        _isStoping = true;
        //gameObject.SetActive(false);
    }

    protected virtual void MoveLogic()
    {
        
    }
    

}
