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
    
    public void SetInitialValues(int flashlightCount, int fireCount, int laserCount)
    {
        flashlightCountText.text = flashlightCount.ToString();
        fireCountText.text = fireCount.ToString();
        laserCountText.text = laserCount.ToString();
    }
    
    public void decreaseFlashlightCount(string lightType)
    {
        switch (lightType)
        {
            case "Flashlight":
                int flashlightCount = int.Parse(flashlightCountText.text);
                flashlightCount--;
                flashlightCountText.text = flashlightCount.ToString();
                break;
            case "Fire":
                int fireCount = int.Parse(fireCountText.text);
                fireCount--;
                fireCountText.text = fireCount.ToString();
                break;
            case "Laser":
                int laserCount = int.Parse(laserCountText.text);
                laserCount--;
                laserCountText.text = laserCount.ToString();
                break;
        }
    }
    
    public IEnumerator ShowInfoText(string lightName, int seconds)
    {
        infoText.text = $"Elinde yeteri kadar {lightName} yok!";
        yield return new WaitForSeconds(seconds);
        infoText.text = "";
    }
}
