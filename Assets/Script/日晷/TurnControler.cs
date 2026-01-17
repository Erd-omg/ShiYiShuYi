using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControler : MonoBehaviour 
{
    public TurnImage turnImageInstance;
    public TurnImageTwo turn2;
    public turnImageThree turn3;
    public GameObject panel;
    private GameObject shadow;
    private GameObject background;

    private void Start()
    {
        shadow = GameObject.Find("shadow");
        background = GameObject.Find("background");
    }

    public void Update()
    {
        if(turnImageInstance.image1Reached90&&turn2.image2Reached90 &&turn3 .image3Reached90 )
        {
            StartCoroutine(ShowPanelAfterTwoSeconds()); // Æô¶¯Ð­³Ì  
        }
    }

    IEnumerator ShowPanelAfterTwoSeconds()
    {
        yield return new WaitForSeconds(2);
        panel.SetActive(true);
        shadow.SetActive(true);
        background.SetActive(true);
    }
}
