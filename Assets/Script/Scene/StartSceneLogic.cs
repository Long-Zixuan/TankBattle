using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLogic : BaseSceneLogic
{
    new void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBottonLogic()
    {
        SceneManager.LoadScene("Scenes/Connect");
    }

    public void quitBottonLogic()
    {
       Application.Quit(); 
    }
    
}
