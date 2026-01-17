using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ruleControl : MonoBehaviour
{
    public List<GameObject> rules;
    public Button next;
    public Button last;
    public int index;

    void Start()
    {
        last.gameObject.SetActive(false);
        next.onClick.AddListener(ActiveNext);
        last.onClick.AddListener(ActiveLast);
    }

    void Update()
    {
    }

    void ActiveNext()
    {
        if(index<rules.Count-1)
        {
            rules[index].SetActive(false);
            index++;
            rules[index].SetActive(true);

            if(index==rules.Count-1)
                next.gameObject.SetActive (false);
        }
        last.gameObject.SetActive (true);
    }

    void ActiveLast()
    {
        if(index>0)
        {
            rules[index].SetActive(false);
            index--;
            rules[index].SetActive(true);

            if(index==0)
                last.gameObject.SetActive (false);
        }
        next.gameObject.SetActive (true);
    }
}
