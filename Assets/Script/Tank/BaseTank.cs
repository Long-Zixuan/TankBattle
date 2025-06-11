using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTank : MonoBehaviour,IObjInScene
{
    public Animator _animator;
    
    public GameObject bulletPrefab;
    [Header("Move")]
    // 坦克移动速度
    public float speed = 2;

    // 坦克转身速度
    public float turnSpeed = 3;
    // 发射炮弹初速度
    public float fireSpeed = 20;
    public string desc = "";

    //public BaseSceneLogic sceneLogic;
    
    protected Rigidbody _rb;
    protected bool _isDie = false;
    
    public bool IsDie
    {
        get { return _isDie; }
        set { _isDie = value; }
    }

    protected AudioSource _move;
    protected AudioSource _fire;
    protected AudioSource _boom;
    
    protected int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    protected bool _isStoping = false;

    protected Transform _tankGun;
  
    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _tankGun = transform.Find("TankGun");
        _move = transform.Find("TankRenderers/TankTracksLeft").GetComponent<AudioSource>();
        _fire = _tankGun.gameObject.GetComponent<AudioSource>();
        _boom = transform.Find("TankRenderers").GetComponent<AudioSource>();
        if (bulletPrefab == null)
        {
            bulletPrefab = GameObject.Find("Bullet");
        }
    }


    protected Vector3 _lastFamePos = Vector3.zero;
    protected Vector3 _thisFamePos = Vector3.zero;
    bool _isMoving = false;
    void PlayAudioLogic()
    {
        _thisFamePos = transform.position;
        if (_lastFamePos == _thisFamePos)
        {
//            print("Tank Stop");
            _move.Stop();
            _isMoving = false;
        }
        else
        {
  //          print("Tank Move");
            if (!_isMoving)
            {
                _move.Play();
                _isMoving = true;
            }
        }
        _lastFamePos = _thisFamePos;
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
    
    public virtual void OnSceneStop()
    {
        
    }
    

}
