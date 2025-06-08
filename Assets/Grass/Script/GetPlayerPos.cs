using System;
using UnityEngine;
 
[ExecuteInEditMode]
public class GetPlayerPos : MonoBehaviour
{
    private GameObject player = null;
    public Material material;
    // Start is called before the first frame update

    private bool findPlayer = false;
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        if (!findPlayer)
        {
            try
            {
                player = GameObject.FindObjectOfType<CtrlTank>().gameObject;
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
            
            if (player != null)
            {
                findPlayer = true;
            }
        }
        else
        {
//            print(player.transform.position);
            material.SetVector("_PlayerPos", player.transform.position);
        }
    }
}
 
 
//LZX completed this script in 2024/05/06
//LZX-TC-VS-2024-05-05-001