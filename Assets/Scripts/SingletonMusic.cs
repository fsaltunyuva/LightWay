using UnityEngine;

public class SingletonMusic : MonoBehaviour
{
    private static SingletonMusic instance = null;
    
    public int birikim = 0;

    public static SingletonMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}