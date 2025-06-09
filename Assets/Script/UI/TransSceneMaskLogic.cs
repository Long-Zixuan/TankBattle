using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TransSceneMaskLogic : MonoBehaviour,IObjInScene
{
    private Camera _cam;//挂载rendertexture的相机
    private Image _im;//显示的image
    RenderTexture _fullBodyRender;
    Texture2D _fullBodyTex;
    static public Sprite S_sprite;
    private BaseSceneLogic _sceneLogic;
    private void Start()
    {
        //_im.gameObject.SetActive(true);

        _sceneLogic = GameObject.FindObjectOfType<BaseSceneLogic>();
        _sceneLogic.AddListener(this);
        _im = GetComponent<Image>();
        
        if (S_sprite == null)
        {
            print("S_sprite is null");
            return;
        }
        _im.sprite = S_sprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneStop()
    {
        StartCoroutine(initCamAndGetFramBuf());
    }

    IEnumerator initCamAndGetFramBuf()
    {
        //S_sprite = null;
        Camera[] activeCams = GameObject.FindObjectsOfType<Camera>();
        Camera mainCam = null;
        foreach (Camera c in activeCams)
        {
            if (c.name != "Effect Camera" && c.isActiveAndEnabled)
            {
                mainCam = c;
            }
        }

        if (mainCam == null)
        {
            print("Main Camera is null");
            yield return null;
        }
        print("main Cam:"+mainCam.name);
        _cam = GameObject.Find("Effect Camera").GetComponent<Camera>();
        _cam.gameObject.transform.position = mainCam.gameObject.transform.position;
        _cam.gameObject.transform.rotation = mainCam.gameObject.transform.rotation;
        print(_cam.gameObject.transform.position);
        print(_cam.gameObject.transform.rotation);
        _cam.fieldOfView = mainCam.fieldOfView;
        if(_cam == null)
            print("Effect Cam is null");
        //yield return new WaitForEndOfFrame();
        yield return new WaitForNextFrameUnit();
        _fullBodyRender = _cam.targetTexture;
        getFrameBuffer();
    }
    
    
    
    
    void getFrameBuffer()
    {
        if (_cam == null)
        {
            print("Cam is null");
        }

        if (_fullBodyRender == null)
        {
            print("Render is null");
        }
        _fullBodyTex = getTexture2d(_fullBodyRender);
        if (_fullBodyTex == null)
        {
            print("BodyTex is null");
        }
        
        //将Texture2D转成Sprite格式
        S_sprite = Sprite.Create(_fullBodyTex, new Rect(0, 0, _fullBodyTex.width, _fullBodyTex.height),
            Vector2.zero);

        print("getFramebuffer");
        print(S_sprite);
        if(S_sprite == null)
            print("S_sprite is null");
    }

    private Texture2D getTexture2d(RenderTexture renderT)
    {
        if (renderT == null)
            return null;
        int width = renderT.width;
        int height = renderT.height;
        Texture2D tex2d = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderT;
        tex2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex2d.Apply();
        return tex2d;
    }
}
