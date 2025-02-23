using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void SkipTutorial()
    {
        SceneManager.LoadScene(1);
    }
    
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject text3;
    
    public void NextText1()
    {
        text1.SetActive(false);
        text2.SetActive(true);
    }
    
    public void NextText2()
    {
        text2.SetActive(false);
        text3.SetActive(true);
    }
    
}
