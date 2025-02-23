using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishDetector : MonoBehaviour
{
    [SerializeField] private Hand _handScript;
    private SingletonMusic _singletonMusic;

    private void Start()
    {
        _singletonMusic = SingletonMusic.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SingletonMusic.Instance.birikim = 0;
                return;
            }


            _singletonMusic.birikim += _handScript.levelPoint;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
    }
}
