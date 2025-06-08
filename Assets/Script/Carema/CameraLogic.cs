using UnityEngine;

/// <summary>

/// �����Ƿ������߼�������λ��

/// ��ⲻ�����Ͱ�����ƶ��������ߵ���ײ���ǰ��

/// </summary>

public class CameraLogic : MonoBehaviour

{

    public Transform player = null;            //��ɫͷ�������ÿ����壩λ����Ϣ

    private Vector3 tagetPostion;       //��������Ŀ���

    private Vector3 ve3;                //ƽ�������ref����

    Quaternion angel;                   //�������Ŀ�����תֵ

    public float speed;                 //����ƶ��ٶ�

    public float upFloat;               //Y����������

    public float backFloat;             //Z�������ǵľ���

    public string[] unColiTags;
    
    bool findPlayer = false;

    void LateUpdate()

    {
        if (!findPlayer)
        {
            try
            {
                player = GameObject.FindObjectOfType<CtrlTank>().gameObject.transform;
            }
            catch (System.Exception e)
            {
                print(e.ToString());
            }
            
            if (player != null)
            {
                findPlayer = true;
            }
        }
        else
        {
            //��¼�����ʼλ��

            tagetPostion = player.position + player.up * upFloat - player.forward * backFloat;

            //[size = 12.6667px]//ˢ�����Ŀ��������

            tagetPostion = Function(tagetPostion);

            //���ǵ��ƶ��Ϳ���

            transform.position = Vector3.SmoothDamp(transform.position, tagetPostion, ref ve3, 0);

            angel = Quaternion.LookRotation(player.position - tagetPostion);

            transform.rotation = Quaternion.Slerp(transform.rotation, angel, speed);
        }

    }

    /// <summary>

    /// ���߼�⣬����������Ƿ����������

    /// </summary>

    /// <param name="v3">�����������߷���ķ���</param>

    /// <returns>�Ƿ��⵽</returns>

    Vector3 Function(Vector3 v3)

    {

        RaycastHit hit;

        if (Physics.Raycast(player.position, v3 - player.position, out hit, 5.0f))

        {

            if (hit.collider.tag != "MainCamera" && isColi(hit.collider.tag))
            {

                v3 = hit.point + transform.forward * 0.5f;

            }

        }

        return v3;

    }


    bool isColi(string tag)
    {
        foreach(var t in unColiTags)
        {
            if( t == tag)
            {
                return false;
            }
        }
        return true;
    }


}