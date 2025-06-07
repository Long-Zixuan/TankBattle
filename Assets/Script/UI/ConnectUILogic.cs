using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ConnectUILogic : MonoBehaviour
{
    public InputField ipInput;
    public InputField connectPortInput;
    public InputField creatPortInput;
    
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

    void printIpAndPort()
    {
       Debug.Log("ip:"+IP+" port:"+port); 
    }

    public void creatAndConnectToServes()
    {
        try
        {
            ServesMain.PORT = int.Parse(creatPortInput.text);
            port = ServesMain.PORT;
        }
        catch (Exception e)
        {
            ServesMain.PORT = 25565;
            Debug.Log(e.ToString());
        }
        try
        {
            Thread serves = new Thread(ServesMain.MainLogic);
            serves.Start();
            printIpAndPort();
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
            IP = ipInput.text;
            port = int.Parse(connectPortInput.text);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        if (IP.Split('.').Length < 4)
        {
            Debug.Log("ip error");
            return;
        }
        if (port <= 1024 || port > 65535)
        {
            Debug.Log("port error");
            port = 25565;
        }
        try
        {
            printIpAndPort();
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
