using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flashlightCountText;
    [SerializeField] private TextMeshProUGUI fireCountText;
    [SerializeField] private TextMeshProUGUI laserCountText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI remainingMoneyText;
    [SerializeField] private TextMeshProUGUI birikimText;

    private void Start()
    {
        birikimText.text = $"birikim: {SingletonMusic.Instance.birikim}<color=#00FF00> $";
    }

    public IEnumerator ShowInfoText(string lightName, int seconds)
    {
        infoText.text = $"{lightName} kullanabilecek kadar puanin yok!";
        yield return new WaitForSeconds(seconds);
        infoText.text = "";
    }

    public void UpdateRemainingMoney(int newMoney)
    {
        remainingMoneyText.text = "Kalan para : " + newMoney + "<color=#00FF00> $";
    }
    
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}
