using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flashlightCountText;
    [SerializeField] private TextMeshProUGUI fireCountText;
    [SerializeField] private TextMeshProUGUI laserCountText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI remainingMoneyText;
    
    public IEnumerator ShowInfoText(string lightName, int seconds)
    {
        infoText.text = $"{lightName} kullanabilecek kadar puanın yok!";
        yield return new WaitForSeconds(seconds);
        infoText.text = "";
    }

    public void UpdateRemainingMoney(int newMoney)
    {
        remainingMoneyText.text = "Kalan para : " + newMoney + "<color=#00FF00> $";
    }
}
