using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private string currentLightType = "Flashlight"; // Others "Fire", "Laser", "Flashlight"
    [SerializeField] private GameObject flashlightPrefab, firePrefab, laserPrefab;
    [SerializeField] private GameObject atesPreview;
    [SerializeField] private GameObject atesOnHand;
    private string[] lightTypes = {"Flashlight", "Fire", "Laser"};
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
    

    private void Start()
    {
        lightList = new CircularLinkedList();
        // foreach (string lightType in lightTypes)
        // {
        //     lightList.AddNode(3, lightType);
        // }
        lightList.AddNode(0, "Flashlight");
        lightList.AddNode(3, "Fire");
        lightList.AddNode(1, "Laser");
        currentNode = lightList.head;
        //currentNode = currentNode.next;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
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
                    if (currentNode.count <= 0)
                    {
                        Debug.Log("No flashlight left");
                        return;
                    }
                    // _flashlightRotator.placeFlashLight();
                    // if(currentNode.count - 1 > 0)
                    //     _flashlightRotator.InstantiateNewFlashlightLight();
                    _flashlightRotator.SecondMethod();
                    currentNode.count--;
                    
                    break;
                case "Fire":
                    if(currentNode.count <= 0) return;
                    Instantiate(firePrefab, atesPreview.transform.position, Quaternion.identity);
                    currentNode.count--;
                    break;
                case "Laser":
                    if(currentNode.count <= 0) return;
                    Debug.Log("Laser Used.");
                    // TODO: Laser
                    currentNode.count--;
                break;
            }
        }

    }

    private void placeFireOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 1);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0.5f);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 0);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 0);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 0);
    }
    
    private void placeLaserOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 0);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 0);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 0);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 1);
    }
    
    private void placeFlashlightOnHand()
    {
        atesOnHandSpriteRenderer.color = new Color(atesOnHandSpriteRenderer.color.r, atesOnHandSpriteRenderer.color.g, atesOnHandSpriteRenderer.color.b, 0);
        atesPreviewSpriteRenderer.color = new Color(atesPreviewSpriteRenderer.color.r, atesPreviewSpriteRenderer.color.g, atesPreviewSpriteRenderer.color.b, 0);
        flashlightSpriteRenderer.color = new Color(flashlightSpriteRenderer.color.r, flashlightSpriteRenderer.color.g, flashlightSpriteRenderer.color.b, 1);
        flashlightLightSpriteRenderer.color = new Color(flashlightLightSpriteRenderer.color.r, flashlightLightSpriteRenderer.color.g, flashlightLightSpriteRenderer.color.b, 1);
        laserSpriteRenderer.color = new Color(laserSpriteRenderer.color.r, laserSpriteRenderer.color.g, laserSpriteRenderer.color.b, 0);
    }
}
