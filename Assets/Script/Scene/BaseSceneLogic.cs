using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLogic : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        MsgManager.Instance.upDateSceneManage();
    }

    // Update is called once per frame
    protected void Update()
    {
        NetManager.Update();
    }
}
