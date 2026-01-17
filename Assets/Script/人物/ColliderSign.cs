using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSign : MonoBehaviour
{
    public GameObject eTip;
    public GameObject walkTip;
    public GameObject pressTip;

    void Start()
    {
        eTip.SetActive(false);
        pressTip.SetActive(false );
        walkTip.SetActive(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        eTip.SetActive(true); 
        pressTip.SetActive(true);
        walkTip.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        eTip.SetActive(true);
        pressTip.SetActive(true );
        walkTip.SetActive(false);
        
    }

    private void OnCollisionExit(Collision collision)
    {
        eTip.SetActive(false);
        pressTip.SetActive(false);
        walkTip.SetActive(true);
    }
}