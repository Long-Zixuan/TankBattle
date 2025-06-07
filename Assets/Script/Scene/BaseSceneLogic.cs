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
    
    public virtual void OnStart(string msgArgs)
    {
        
    }

    public virtual void OnEnter(string msgArgs)
    {
	    
    }

    public virtual void OnList (string msgArgs) 
    {
		
	}

	public virtual void OnRotate (string msgArgs) {
		
	}
	
	public virtual void OnMove (string msgArgs) {

	}

	public virtual void OnLeave (string msgArgs) {

	}

	public virtual void OnFire (string msgArgs) {
	
	}

	public virtual void OnDie (string msgArgs) {
		
	}
	
	
}
