using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject myBag;
    bool isOpen;

    private void Update()
    {
        OpenMyBag();
    }
    void OpenMyBag()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }
}
