using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlTank : BaseTank
{
    /*public KeyCode x_plus = KeyCode.W;
    public KeyCode x_minus = KeyCode.S;
    public KeyCode y_plus = KeyCode.D;
    public KeyCode y_minus = KeyCode.A;*/
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        FireLogic();
    }

    private float tmpX;
    private float tmpZ;

    void FireLogic()
    {
        if(Input.GetButtonDown("Jump"))
        {
            // 创建一颗子弹
            Rigidbody bullet = Instantiate(bulletPrefab).GetComponent<Rigidbody>();

            bullet.transform.forward = _tankGun.forward;         // 炮弹的朝向，与炮口朝向一致
            bullet.transform.position = _tankGun.position;       // 炮弹位置等于炮口位置
            bullet.velocity = new Vector3(this.transform.forward.x,0,transform.forward.z).normalized * fireSpeed;      // 炮弹的初速度，受fireSpeed字段控制
            NetManager.Send("Fire|"+desc+","+transform.eulerAngles.x+","+transform.eulerAngles.y+","
                            +transform.eulerAngles.z+","+transform.position.x+","+transform.position.y+","+transform.position.z);
        }

    }

    float normalFloat(float f)
    {
        if (f > 0)
        {
            return 1;
        }
        else if (f < 0)
        {
            return -1;
        }
        return 0;
    }
    
    protected override void MoveLogic()
    {
        /*if (Input.GetKeyDown(x_plus))
        {
            string sendMSG = "Move|"+NetManager.GetDesc()+"1";
        }

        if (Input.GetKeyDown(x_minus))
        {
            string sendMSG = "Move|"+NetManager.GetDesc()+"-1";
            NetManager.Send(sendMSG);
        }

        if (Input.GetKeyDown(y_plus))
        {
            string sendMSG = "Rotate|" +NetManager.GetDesc()+ "1";
            NetManager.Send(sendMSG);
        }

        if (Input.GetKeyDown(y_minus))
        {
            string sendMSG = "Rotate|"+NetManager.GetDesc()+ "-1";
            NetManager.Send(sendMSG);
        }

        if (Input.GetKeyUp(x_plus) || Input.GetKeyUp(x_minus))
        {
            string sendMSG = "Move|" + "0";
            NetManager.Send(sendMSG);
        }

        if (Input.GetKeyUp(y_plus) || Input.GetKeyUp(y_minus))
        {
            string sendMSG = "Rotate|" + "0";
            NetManager.Send(sendMSG);
        }*/
        float x = Input.GetAxis("Horizontal");          // 横向输入，键盘的A、D键
        float z = Input.GetAxis("Vertical");            // 纵向输入，键盘的W、S键
        x = normalFloat(x);
        z = normalFloat(z);

        if (Mathf.Abs(x - tmpX) >= 0.1f)
        {
            string sendMSG = "Rotate|" + NetManager.GetDesc()+"," + x + ","+transform.eulerAngles.x+","+transform.eulerAngles.y+","+transform.eulerAngles.z;;
            NetManager.Send(sendMSG);
            tmpX = x;
        }
        if (Mathf.Abs(z - tmpZ) >= 0.1f)
        {
            string sendMSG = "Move|" + NetManager.GetDesc()+"," + z + "," + transform.position.x + "," + transform.position.y + "," + transform.position.z;
            NetManager.Send(sendMSG);
            tmpZ = z;
        }
        
        transform.Translate(Vector3.forward * speed * z * Time.deltaTime);
        // 设置坦克刚体的速度
      //  _rb.velocity = transform.forward * z * speed;
        // 设置坦克刚体的角速度，就转起来了
        _rb.angularVelocity = new Vector3(0, x, 0) * turnSpeed;
    }
}
