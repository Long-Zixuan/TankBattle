using UnityEngine;
using System.Collections;
using System.IO;
using System;

public delegate void CallBack();//����ί�лص������ȹر�UI����ȡ��û��UI�Ļ���
/// <summary>
/// ��ͼ������
/// </summary>
public class ScreenTool
{
    private static ScreenTool _instance;
    public static ScreenTool Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScreenTool();
            return _instance;
        }
    }
    public string UrlRelativeToAbsolute(string relative)
    {
        string absolutePath = System.IO.Path.GetFullPath(relative);
        return absolutePath;
    }
    /// <summary>
    /// UnityEngine�Դ�����Api��ֻ�ܽ�ȫ��,�������·���;���·��������
    /// </summary>
    /// <param name="fileName">�ļ���ַ�Լ�����������׺����</param>
    public void ScreenShotFile(string fileName)
    {
        UnityEngine.ScreenCapture.CaptureScreenshot(UrlRelativeToAbsolute(fileName));//��ͼ�������ͼ�ļ�
        Debug.Log(string.Format("��ȡ��һ��ͼƬ: {0}", fileName));

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//ˢ��Unity���ʲ�Ŀ¼
#endif
    }
    /// <summary>
    /// UnityEngine�Դ�����Api��ֻ�ܽ�ȫ��
    /// </summary>
    /// <param name="fileName">�ļ���</param>
    /// <param name="callBack">��ͼ��ɻص�</param>
    /// <returns>Э��</returns>
    public IEnumerator ScreenShotTex(string fileName, CallBack callBack = null)
    {
        yield return new WaitForEndOfFrame();//�ȵ�֡��������Ȼ�ᱨ��
        Texture2D tex = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();//��ͼ����Texture2D����
        byte[] bytes = tex.EncodeToPNG();//���������ݣ�ת����һ��pngͼƬ
        System.IO.File.WriteAllBytes(fileName, bytes);//д������
        Debug.Log(string.Format("��ȡ��һ��ͼƬ: {0}", fileName));

        callBack?.Invoke();
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//ˢ��Unity���ʲ�Ŀ¼
#endif
    }
    /// <summary>
    /// ��ȡ��Ϸ��Ļ�ڵ�����
    /// </summary>
    /// <param name="rect">��ȡ������Ļ���½�Ϊ0��</param>
    /// <param name="fileName">�ļ���</param>
    /// <param name="callBack">��ͼ��ɻص�</param>
    /// <returns></returns>
    public IEnumerator ScreenCapture(Rect rect, string fileName, CallBack callBack = null)
    {
        yield return new WaitForEndOfFrame();//�ȵ�֡��������Ȼ�ᱨ��
        Texture2D tex = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, false);//�½�һ��Texture2D����
        tex.ReadPixels(rect, 0, 0);//��ȡ���أ���Ļ���½�Ϊ0��
        tex.Apply();//����������Ϣ

        byte[] bytes = tex.EncodeToPNG();//���������ݣ�ת����һ��pngͼƬ
        System.IO.File.WriteAllBytes(fileName, bytes);//д������
        Debug.Log(string.Format("��ȡ��һ��ͼƬ: {0}", fileName));

        callBack?.Invoke();
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//ˢ��Unity���ʲ�Ŀ¼
#endif
    }
    /// <summary>
    /// ���������������н�ͼ�������Ҫ���������������ӣ��ɽ�ȡ�������ĵ��ӻ���
    /// </summary>
    /// <param name="camera">����ͼ�����</param>
    /// <param name="width">��ȡ��ͼƬ���</param>
    /// <param name="height">��ȡ��ͼƬ�߶�</param>
    /// <param name="fileName">�ļ���</param>
    /// <returns>����Texture2D����</returns>
    public Texture2D CameraCapture(Camera camera, Rect rect, string fileName)
    {
        RenderTexture render = new RenderTexture((int)rect.width, (int)rect.height, -1);//����һ��RenderTexture���� 

        camera.gameObject.SetActive(true);//���ý�ͼ���
        camera.targetTexture = render;//���ý�ͼ�����targetTextureΪrender
        camera.Render();//�ֶ�������ͼ�������Ⱦ

        RenderTexture.active = render;//����RenderTexture
        Texture2D tex = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, false);//�½�һ��Texture2D����
        tex.ReadPixels(rect, 0, 0);//��ȡ����
        tex.Apply();//����������Ϣ

        camera.targetTexture = null;//���ý�ͼ�����targetTexture
        RenderTexture.active = null;//�ر�RenderTexture�ļ���״̬
        UnityEngine.Object.Destroy(render);//ɾ��RenderTexture����

        byte[] bytes = tex.EncodeToPNG();//���������ݣ�ת����һ��pngͼƬ
        System.IO.File.WriteAllBytes(fileName, bytes);//д������
        Debug.Log(string.Format("��ȡ��һ��ͼƬ: {0}", fileName));

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//ˢ��Unity���ʲ�Ŀ¼
#endif

        return tex;//����Texture2D���󣬷�����Ϸ��չʾ��ʹ��
    }



    private void CheckFold(string path = "Assets/../ScreenShot/")
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public void _SaveCamTexture(Camera cam,string path)
    {
        RenderTexture rt;
        rt = cam.targetTexture;
        if (rt != null)
        {
            _SaveRenderTexture(rt,path);
            rt = null;
        }
        else
        {
            GameObject camGo = new GameObject("camGO");
            Camera tmpCam = camGo.AddComponent<Camera>();
            tmpCam.CopyFrom(cam);
            // rt = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
            rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);

            tmpCam.targetTexture = rt;
            tmpCam.Render();
            _SaveRenderTexture(rt,path);
            MonoBehaviour.Destroy(camGo);
            //rt.Release();
            RenderTexture.ReleaseTemporary(rt);
            //Destroy(rt);
            rt = null;
        }

    }
    private void _SaveRenderTexture(RenderTexture rt,string fold)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D jpg = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        jpg.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        jpg.Apply();

        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        png.Apply();
        RenderTexture.active = active;
        byte[] bytesPNG = png.EncodeToPNG();

        byte[] bytesJPG = jpg.EncodeToJPG();

        /*if (!Directory.Exists("Assets/../ScreenShot/"))
        {
            Directory.CreateDirectory("Assets/../ScreenShot/");
        }*/
        CheckFold(fold);

        string pathPNG = string.Format(fold+"LZX_ToonShaderTest_rt_{0}_{1}_{2}_{3}_{4}_{5}.png", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        string pathJPG = string.Format(fold+"LZX_ToonShaderTest_rt_{0}_{1}_{2}_{3}_{4}_{5}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        //string pathPNG = string.Format("/LZX_ToonShaderTest_rt_{0}_{1}_{2}_{3}_{4}_{5}.png", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //string pathJPG = string.Format("/LZX_ToonShaderTest_rt_{0}_{1}_{2}_{3}_{4}_{5}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        FileStream fsPNG = File.Open(pathPNG, FileMode.Create);
        FileStream fsJPG = File.Open(pathJPG, FileMode.Create);
        BinaryWriter writerPNG = new BinaryWriter(fsPNG);
        BinaryWriter writerJPG = new BinaryWriter(fsJPG);
        writerJPG.Write(bytesJPG);
        writerPNG.Write(bytesPNG);
        writerJPG.Flush();
        writerPNG.Close();
        fsPNG.Close();
        fsJPG.Close();
        MonoBehaviour.Destroy(png);
        png = null;
        MonoBehaviour.Destroy(jpg);
        jpg = null;
        Debug.Log("����ɹ���" + pathPNG);
        Debug.Log("����ɹ���" + pathJPG);
    }
}
