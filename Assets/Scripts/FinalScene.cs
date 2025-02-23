using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _birikimText;

    private void Start()
    {
        _birikimText.text = $"<wave> {SingletonMusic.Instance.birikim} <color=#00FF00>$";
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void GoToTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
