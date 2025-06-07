using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class ConnectUILogic : MonoBehaviour
{
    [Header("Massage")]
    public Text massage;
    [Header("Input")]
    public InputField ipInput;
    public InputField connectPortInput;
    public InputField creatPortInput;
    [Header("UI")]
    public GameObject connectUI;
    [Header("Net")]
    
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

    string getIp()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string ipStr = "Unkown";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine("IP Address = " + ip.ToString());
                ipStr = ip.ToString();
            }
        }

        return ipStr;
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
            
            NetManager.Send("Enter|"+NetManager.GetDesc());
            connectUI.SetActive(false);
            string msg = "游戏已经在局域网内启动，请输入IP地址和端口号连接。IP：" + getIp() + " 端口:" + port;;
            massage.text = msg;
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
            massage.text = "连接中";
            connectUI.SetActive(false);
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
        massage.text = "连接超时";
        Debug.Log("time out");
    }
    
}
