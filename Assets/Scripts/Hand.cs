using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class Hand : MonoBehaviour
{
    private string currentLightType = "Flashlight"; // Others "Fire", "Laser", "Flashlight"
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject atesPreview;
    [SerializeField] private GameObject atesOnHand;
    private CircularLinkedList lightList;
    int currentCount = 0;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject laser;
    private CircularLinkedList.Node currentNode;
    [SerializeField] private FlashlightRotator _flashlightRotator;
    [SerializeField] private SpriteRenderer atesPreviewSpriteRenderer;
    [SerializeField] private SpriteRenderer atesOnHandSpriteRenderer;
    [SerializeField] private SpriteRenderer flashlightSpriteRenderer;
    [SerializeField] private SpriteRenderer flashlightLightSpriteRenderer;
    [SerializeField] private SpriteRenderer laserSpriteRenderer;
    [SerializeField] private SpriteRenderer laserGunSpriteRenderer;
    [SerializeField] private int laserDisappearDuration = 5;
    [SerializeField] private GameObject laserDisappearPivot;
    
    [FormerlySerializedAs("flashlightCount")] [SerializeField] private int flashlightCost = 3;
    [FormerlySerializedAs("fireCount")] [SerializeField] private int fireCost = 5;
    [FormerlySerializedAs("laserCount")] [SerializeField] private int laserCost = 4;
    [SerializeField] private int levelPoint = 10;
    [SerializeField] private UIController uiController;
    private bool isFlashlightTutorial = false;
    private bool isFireTutorial = false;
    private bool isLaserTutorial = false;

    [SerializeField] private AudioClip flashlightTickSound;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioSource _audioSource;
    

    private void Start()
    {
        lightList = new CircularLinkedList();
        // TODO: Change according to the level's requirements
        lightList.AddNode(flashlightCost, "Flashlight");
        lightList.AddNode(fireCost, "Fire");
        lightList.AddNode(laserCost, "Laser");
        currentNode = lightList.head;
        
        if(SceneManager.GetActiveScene().name == "Tutorial Flash")
        {
            isFlashlightTutorial = true;
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial Fire")
        {
            isFireTutorial = true;
            currentLightType = "Fire";
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial Laser")
        {
            isLaserTutorial = true;
            currentLightType = "Laser";
        }
        
        uiController.UpdateRemainingMoney(levelPoint);
        
        Debug.Log($"isFlashlightTutorial: {isFlashlightTutorial}, isFireTutorial: {isFireTutorial}, isLaserTutorial: {isLaserTutorial}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (isFlashlightTutorial || isFireTutorial || isLaserTutorial)
            {
                return;
            }
            currentNode = currentNode.next;
            (currentCount, currentLightType) = (currentNode.count, currentNode.name);
            
            switch (currentLightType)
            {
                case "Fire":
                    placeFireOnHand();
                    break;
                case "Laser":
                    placeLaserOnHand();
                    break;
                case "Flashlight":
                    placeFlashlightOnHand();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (currentLightType)
            {
                case "Flashlight":
                    //if (currentNode.count <= 0)
                    if(levelPoint - flashlightCost < 0)
                    {
                        StartCoroutine(uiController.ShowInfoText("El Feneri", 2));
                        return;
                    }
                    // _flashlightRotator.placeFlashLight();
                    // if(currentNode.count - 1 > 0)
                    //     _flashlightRotator.InstantiateNewFlashlightLight();
                    _audioSource.PlayOneShot(flashlightTickSound);
                    _flashlightRotator.SecondMethod();
                    //currentNode.count--;
                    levelPoint -= flashlightCost;
                    uiController.UpdateRemainingMoney(levelPoint);
                    Debug.Log("Current level point: " + levelPoint);
                    //uiController.decreaseFlashlightCount("Flashlight");
                    break;
                case "Fire":
                    //if (currentNode.count <= 0)
                    if(levelPoint - fireCost < 0)    
                    {
                        StartCoroutine(uiController.ShowInfoText("Ates", 2));
                        return;
                    }
                    Instantiate(firePrefab, atesPreview.transform.position, Quaternion.identity);
                    //currentNode.count--;
                    levelPoint -= fireCost;
                    uiController.UpdateRemainingMoney(levelPoint);
                    Debug.Log("Current level point: " + levelPoint);
                    //uiController.decreaseFlashlightCount("Fire");
                    break;
                case "Laser":
                    // if (currentNode.count <= 0)
                    if(levelPoint - laserCost < 0)    
                    {
                        StartCoroutine(uiController.ShowInfoText("Lazer", 2));
                        return;
                    }
                    
                    Vector3 laserPosition = laser.transform.position;
                    Quaternion laserRotation = laser.transform.rotation;
                    Vector3 laserScale = laser.transform.lossyScale;
                    
                    _audioSource.PlayOneShot(laserSound, 0.6f);
                    
                    GameObject newLaser = Instantiate(laser, laserPosition, laserRotation);
                    newLaser.transform.localScale = laserScale;
                    newLaser.transform.SetParent(null);
                    
                    newLaser.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
                    
                    StartCoroutine(DestroyLaserFromLeft(newLaser));
                    //currentNode.count--;
                    levelPoint -= laserCost;
                    uiController.UpdateRemainingMoney(levelPoint);
                    Debug.Log("Current level point: " + levelPoint);
                    //uiController.decreaseFlashlightCount("Laser");
                break;
            }
        }

    }
    
    IEnumerator DestroyLaserFromLeft(GameObject newLaser)
    {
        float time = 0;
        float startScaleX = newLaser.transform.localScale.x;
        
        while (time < laserDisappearDuration)
        {
            time += Time.deltaTime;
            float scaleX = Mathf.Lerp(startScaleX, 0, time / laserDisappearDuration);
            newLaser.transform.localScale = new Vector3(scaleX, newLaser.transform.lossyScale.y, newLaser.transform.lossyScale.z);
            yield return null;
        }
        Destroy(newLaser);
    }

    private void placeFireOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 1);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0.5f);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 0);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 0);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 0);
        laserGunSpriteRenderer.color = new Color(laserGunSpriteRenderer.color.r, laserGunSpriteRenderer.color.g, laserGunSpriteRenderer.color.b, 0);
    }
    
    private void placeLaserOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 0);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 0);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 0);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 1);
        laserGunSpriteRenderer.color = new Color(laserGunSpriteRenderer.color.r, laserGunSpriteRenderer.color.g, laserGunSpriteRenderer.color.b, 1);
    }
    
    private void placeFlashlightOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 0);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 1);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 1);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 0);
        laserGunSpriteRenderer.color = new Color(laserGunSpriteRenderer.color.r, laserGunSpriteRenderer.color.g, laserGunSpriteRenderer.color.b, 0);
    }
    
}
