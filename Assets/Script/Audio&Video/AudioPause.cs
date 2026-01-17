using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPause : MonoBehaviour
{
    private Button button;
    private GameObject stop;
    private AudioSource player;

    private void Start()
    {
        GameObject buttonObj = GameObject.Find("Audio");
        button= buttonObj.GetComponent<Button>();
        stop = GameObject.Find("Stop");

        button.onClick.AddListener(Pause);
        player = GetComponent<AudioSource>();
        stop.SetActive(false);
    }

    void Pause()
    {
        if (player.isPlaying)
        {
            player.Pause();
            stop.SetActive(true);
        }
        else
        {
            player.UnPause();
            stop.SetActive(false);
        }
    }
}
