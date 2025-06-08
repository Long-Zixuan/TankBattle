using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralFadeScript : MonoBehaviour
{
    //控制任意对象淡入淡出，延时，以及淡出后是否跳转场景

    [Header("淡入，延时，淡出")] public float fadeInDuration = 1f; // 淡入时长
    public float delayBeforeFadeOut = 2f; // 延迟淡出时长
    public float fadeOutDuration = 1f; // 淡出时长
    [Header("跳转场景")] public bool shouldLoadNextScene = true; // 控制是否在淡出后跳转场景的开关
    public string nextSceneName; // 下一个场景的名称

    /*private bool fadeInFinished = false; // 是否完成淡入
    private float timer; // 计时器
    private float delayTimer = 0f; // 延迟计时器
    private CanvasRenderer[] renderers;*/ // 存储所有相关的渲染器
    private float timer;
    public float swithLogoTime = 1;

    public GameObject[] logos;

    void Start()
    {
        if (logos.Length == 0)
        {
            return;
        }
        foreach (var logo in logos)
        {
            logo.SetActive(false);
        }
        logos[0].SetActive(true);
    }

    void Update()
    {
        fadeInLogic();
    }

    private int logoIndex = 0;
    void fadeInLogic()
    {
        timer += Time.deltaTime;
        if (timer > swithLogoTime)
        {
            timer = 0;
            logos[logoIndex].SetActive(false);
            logoIndex++;
            if (logoIndex >= logos.Length && shouldLoadNextScene)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (logoIndex < logos.Length)
            {
                logos[logoIndex].SetActive(true);
            }
        }
    }
}