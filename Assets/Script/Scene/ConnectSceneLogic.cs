using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectSceneLogic : BaseSceneLogic
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //chatTextLogic();
    }
    
    public override void OnEnter (string msgArgs) {
        Debug.Log("OnEnter " + msgArgs);
        //解析参数
        string[] split = msgArgs.Split(',');
        string desc = split[0];
        /*float x = float.Parse(split[1]);
        float y = float.Parse(split[2]);
        float z = float.Parse(split[3]);
        float eulY = float.Parse(split[4]);*/
        //是自己
        if(desc == NetManager.GetDesc())
            return;
        //添加一个角色
        //GameObject obj = (GameObject)Instantiate(tankPrefab);
        //obj.transform.position = new Vector3(x, y, z);
        //obj.transform.eulerAngles = new Vector3(0, eulY, 0);
        //BaseTank h = obj.AddComponent<SyncTank>();
        //h.desc = desc;
        //otherHumans.Add(desc, h);
    }

    public override void OnStart(string msgArgs)
    {
        string scIndex = msgArgs;
        //SceneManager.LoadScene(scIndex);
        SceneManager.LoadScene("Scenes/SampleScene");
    }
    
    
}
