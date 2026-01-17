using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    public GameObject last;
    public AudioClip clip1;
    public AudioClip clip2;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip1;
        audioSource.Play();
    }

    void Update()
    {
        if (last!=null&&!last.activeSelf)
        {
            audioSource.clip = clip2;
            audioSource.Play();
            last =null;
        }
    }

    public void Button()
    {
        audioSource.clip = clip2;
        audioSource.Play();
    }
}
