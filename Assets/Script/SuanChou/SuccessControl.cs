using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessControl : MonoBehaviour
{
    public GameObject LevelsuccessPanel;
    private GameObject shadow;
    private GameObject background;
    private void Start()
    {
        shadow = GameObject.Find("shadow");
        background = GameObject.Find("Background");
    }

    public void onClick()
    {
        LevelsuccessPanel.SetActive(false);
        shadow.SetActive(false);
        background.SetActive(false);

        SuanChou.instance.LevelSuccessed = false;
    }
}
