using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WASDSoundPlayer : MonoBehaviour
{
    // 音效剪辑，可在Inspector面板中赋值
    public AudioClip moveSound;

    // 音频源组件
    private AudioSource audioSource;

    // 标记是否正在播放移动音效
    private bool isPlaying = false;

    void Start()
    {
        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 设置音频源属性
        audioSource.playOnAwake = false;
        audioSource.loop = true; // 设置为循环播放
        audioSource.clip = moveSound; // 预先设置音效
    }

    void Update()
    {
        // 检测WASD任意键是否被按住
        bool isKeyHeld = Input.GetKey(KeyCode.W) ||
                         Input.GetKey(KeyCode.A) ||
                         Input.GetKey(KeyCode.S) ||
                         Input.GetKey(KeyCode.D);

        // 如果按键被按住且音效未播放，则开始播放
        if (isKeyHeld && !isPlaying)
        {
            if (moveSound != null)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
        // 如果按键未被按住但音效正在播放，则停止播放
        else if (!isKeyHeld && isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
}

