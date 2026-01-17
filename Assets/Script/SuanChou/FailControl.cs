using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailControl : MonoBehaviour
{
    public GameObject LevelfailPanel;
    private GameObject shadow;
    private GameObject background;
    private void Start()
    {
        shadow = GameObject.Find("shadow");
        background = GameObject.Find("Background");
    }

    public void onClick()
    {
        LevelfailPanel.SetActive(false);
        shadow.SetActive(false);
        background.SetActive(false);
    }
}
