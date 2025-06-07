using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private Rigidbody rb;

    public float Speed
    {
        set => _speed = value;
    }
    
    public float destoryTime;
    public Color _color;

   // private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
       // _velocity = rb.velocity;
    }

    public void Init(Color color)
    {
        GetComponent<Renderer>().material.color = color;
        Invoke("SelfDestory", destoryTime);
    }

    private void Awake()
    {
        //_velocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 Reflect(Vector3 dir, Vector3 nornal)
    {
        return dir - 2 * Vector3.Dot(dir, nornal) * nornal;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            BaseTank tank = other.gameObject.GetComponent<BaseTank>();
            tank.Attacked();
            SelfDestory();
        }

       /* if (other.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Reflect(_velocity, other.contacts[0].normal).normalized * _speed;
            _velocity = rb.velocity;
        }*/
    }

    void SelfDestory()
    {
       Destroy(gameObject); 
    }
    
    
}
