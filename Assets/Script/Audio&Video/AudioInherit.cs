using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioInherit : MonoBehaviour
{
    private static AudioInherit bgm;
    private AudioSource bgmSourse;
    public string EndSceneName;

    private void Awake()
    {
        //只保留初始场景的实例
        if (bgm == null)
        {
            bgm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgmSourse = gameObject.GetComponent<AudioSource>();
    }

    //监听添加场景事件
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //监听移除场景事件
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == EndSceneName)
        {
            Destroy(gameObject);
        }
    }
}
