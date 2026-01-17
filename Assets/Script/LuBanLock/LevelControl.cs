using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string nextScene;

    public void NextButton()
    {
        SceneManager.LoadScene(nextScene);
    }
}
