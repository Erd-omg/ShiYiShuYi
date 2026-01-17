using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EControl : MonoBehaviour
{
    public string SceneName;

    public Image image;
    [SerializeField] private float alpha;
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        Fadeto(SceneName);
    }
    public void Fadeto(string _sceneName)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Fadeout(_sceneName));
        }
    }

    IEnumerator FadeIn()
    {
        alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            image.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);//wait for one second to execute next function
        }
    }
    IEnumerator Fadeout(string sceneName)
    {
        alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            image.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0); //yield return new wait forSeconds;
        }
        SceneManager.LoadScene(SceneName);
    }

}
