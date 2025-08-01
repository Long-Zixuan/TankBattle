using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgManager
{
	private readonly static object lockObj = new object();
    private static volatile MsgManager _instance;

    public static MsgManager Instance
    {
        get
        {
            if (_instance == null)
            {
	            lock (lockObj)
	            {
		            if (_instance == null)
		            {
			            _instance = new MsgManager();
		            }
	            }
            }
            return _instance;
        }
    }
    private BaseSceneLogic sceneLogic;

    private MsgManager()
    {
        NetManager.AddListener("Enter", OnEnter);
        NetManager.AddListener("List", OnList);
        NetManager.AddListener("Move", OnMove);
        NetManager.AddListener("Rotate", OnRotate);
        NetManager.AddListener("Leave", OnLeave);
        NetManager.AddListener("Fire", OnFire);
        NetManager.AddListener("Die", OnDie);
        NetManager.AddListener("Start", OnStart);
        //sceneLogic = GameObject.FindObjectOfType<BaseSceneLogic>();
        sceneLogic = BaseSceneLogic.Instance;

        //NetManager.AddListener("GetChatMSG",getChatMSG);
    }

    public void upDateSceneManage()
    {
	    //sceneLogic = GameObject.FindObjectOfType<BaseSceneLogic>();
	    sceneLogic = BaseSceneLogic.Instance;
    }

    public void upDateSceneManage(BaseSceneLogic logic)
    {
	    sceneLogic = logic;
    }
    
    void OnEnter (string msgArgs) 
    {
	    MonoBehaviour.print("OnEnter");
	    /*try
	    {
		    ConnectSceneLogic csl = (ConnectSceneLogic)sceneLogic;
		    csl.OnEnter(msgArgs);
	    }
	    catch (System.Exception e)
	    {
		    Debug.Log("OnEnter error:" + e.Message);  
	    }*/
	    sceneLogic.OnEnter(msgArgs);
	}

	void OnList (string msgArgs) {
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnList(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnList error:" + e.Message);  
		}*/
		sceneLogic.OnList(msgArgs);
	}

	void OnRotate (string msgArgs) 
	{
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnRotate(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnRotate error:" + e.Message);  
		}*/
		sceneLogic.OnRotate(msgArgs);
		
	}
	
	void OnMove (string msgArgs) 
	{
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnMove(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnMove error:" + e.Message);  
		}*/
		sceneLogic.OnMove(msgArgs);
	}

	void OnLeave (string msgArgs) 
	{
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnLeave(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnLeave error:" + e.Message);  
		}*/
		sceneLogic.OnLeave(msgArgs);
	}

	void OnFire (string msgArgs) 
	{
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnFire(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnFire error:" + e.Message);  
		}*/
		sceneLogic.OnFire(msgArgs);
	}

	void OnDie (string msgArgs) 
	{
		/*try
		{
			SceneMainLogic sml = (SceneMainLogic)sceneLogic;
			sml.OnDie(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnDie error:" + e.Message);  
		}*/
		sceneLogic.OnDie(msgArgs);
	}

	void OnStart(string msgArgs)
	{
		/*try
		{
			MonoBehaviour.print("OnStart");
			ConnectSceneLogic csl = (ConnectSceneLogic)sceneLogic;
			csl.OnStart(msgArgs);
		}
		catch (System.Exception e)
		{
			Debug.Log("OnStart error:" + e.Message);  
		}*/
		sceneLogic.OnStart(msgArgs);
	}
	
	
}
