using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTank : BaseTank
{
    private float moveX;
    
    private float moveZ;

    public void Fire(Vector3 rotate, Vector3 position)
    {
        transform.eulerAngles = rotate;
        transform.position = position;
        GameObject bulletObj = Instantiate(bulletPrefab);
        Bullet bulLogic = bulletObj.GetComponent<Bullet>();
        bulLogic.Init(Color.black);
        Rigidbody bullet = bulletObj.GetComponent<Rigidbody>();

        bullet.transform.forward = _tankGun.forward;         // 炮弹的朝向，与炮口朝向一致
        bullet.transform.position = _tankGun.position;       // 炮弹位置等于炮口位置
        bullet.velocity = new Vector3(this.transform.forward.x,0,transform.forward.z).normalized * fireSpeed;      // 炮弹的初速度，受fireSpeed字段控制
    }

    public float MoveX
    {
        set
        {
            moveX = value;
        }
    }
    
    public float MoveZ
    {
        set
        {
            moveZ = value;
        }
    }
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void MoveLogic()
    {
        // 设置坦克刚体的速度
        transform.Translate(Vector3.forward * speed * moveZ * Time.deltaTime);
        // 设置坦克刚体的角速度，就转起来了
        _rb.angularVelocity = new Vector3(0, moveX, 0) * turnSpeed;
    }
}
