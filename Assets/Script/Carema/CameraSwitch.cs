using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CameraSwitch : MonoBehaviour
{

    public GameObject[] cameras;
    public string[] shotcuts;
    public bool changeAudioListener = true;

    void Start()
    {
        SwitchCamera(0);
    }
    void Update()
    {
        int i = 0;
        for (i = 0; i < cameras.Length; i++)
        {
            
            if (Input.GetKeyUp(shotcuts[i]))
                SwitchCamera(i);
        }
    }

    void SwitchCamera(int index)
    {
        int i = 0;
        for (i = 0; i < cameras.Length; i++)
        {
            if (i != index)
            {
                if (changeAudioListener)
                {
                    cameras[i].GetComponent<AudioListener>().enabled = false;
                }
                cameras[i].GetComponent<Camera>().enabled = false;
                //cameras[i].SetActive(false);
//                cameras[i].GetComponent<SaveCamTexture>().enabled = false; 
            }
            else
            {
                if (changeAudioListener)
                {
                    cameras[i].GetComponent<AudioListener>().enabled = true;
                }
                cameras[i].GetComponent<Camera>().enabled = true;
  //              cameras[i].GetComponent<SaveCamTexture>().enabled = true;
                //cameras[i].SetActive(true);
            }
        }
    }
}