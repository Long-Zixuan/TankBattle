using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLogic : MonoBehaviour
{
	static protected volatile BaseSceneLogic _instance;

	static public BaseSceneLogic Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<BaseSceneLogic>();
				print("Find SceneLogic");
				if (_instance == null)
				{
					Debug.LogWarning("SceneLogic is null");
				}
			}
			return _instance;
		}
	}
	
	protected List<IObjInScene>listeners = new List<IObjInScene>();

	public void AddListener(IObjInScene listener)
	{
		listeners.Add(listener);
	}
	protected bool _isStart = false;

	public bool IsStrt
	{
		get
		{
			return _isStart;
		}
	}

	protected void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			if (_instance != this)
			{
				Destroy(this);
				Debug.LogWarning("SceneManager only have one instance");
				return;
			}
			print("Init yet");
		}
		MsgManager.Instance.upDateSceneManage(this);
	}

	// Start is called before the first frame update
    protected void Start()
    {
	    //_instance = this;
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
