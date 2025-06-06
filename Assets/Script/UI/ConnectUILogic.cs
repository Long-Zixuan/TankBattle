using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ConnectUILogic : MonoBehaviour
{
    public string IP = "127.0.0.1";
    public int port = 8888;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void creatAndConnectToServes()
    {
        try
        {
            Thread serves = new Thread(ServesMain.MainLogic);
            serves.Start();
            NetManager.Connect(IP, port);
            Invoke("timeOut", 5f);
            NetManager.Send("Enter|"+NetManager.GetDesc());
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void connectToServer()
    {
        try
        {
            NetManager.Connect(IP, port);
            Invoke("timeOut", 5f);
            NetManager.Send("Enter|"+NetManager.GetDesc());
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void timeOut()
    {
        Debug.Log("time out");
    }
    
}
