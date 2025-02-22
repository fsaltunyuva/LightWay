using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool isFlashlightPlaced = false, isFirePlaced = false, isLaserPlaced = false;
    private string currentLightType = "Flashlight"; // Others "Fire", "Laser", "Flashlight"
    [SerializeField] private GameObject flashlightPrefab, firePrefab, laserPrefab;
    [SerializeField] private GameObject atesPreview;
    [SerializeField] private GameObject atesOnHand;
    private string[] lightTypes = {"Flashlight", "Fire", "Laser"};
    private int index = 1;
    private CircularLinkedList lightList;
    int currentCount = 0;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject laser;
    private CircularLinkedList.Node currentNode;
    [SerializeField] private FlashlightRotator _flashlightRotator;

    private void Start()
    {
        lightList = new CircularLinkedList();
        foreach (string lightType in lightTypes)
        {
            lightList.AddNode(3, lightType);
        }
        currentNode = lightList.head;
        currentNode = currentNode.next;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            (currentCount, currentLightType) = (currentNode.count, currentNode.name);
            currentNode = currentNode.next;
            
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
                    _flashlightRotator.placeFlashLight();
                    break;
                case "Fire":
                    Instantiate(firePrefab, atesPreview.transform.position, Quaternion.identity);
                    break;
                case "Laser":
                    // TODO: Laser
                break;
            }
        }

    }

    private void placeFireOnHand()
    {
        atesOnHand.SetActive(true);
        atesPreview.SetActive(true);
        flashlight.SetActive(false);
        laser.SetActive(false);
    }
    
    private void placeLaserOnHand()
    {
        laser.SetActive(true);
        atesOnHand.SetActive(false);
        atesPreview.SetActive(false);
        flashlight.SetActive(false);
    }
    
    private void placeFlashlightOnHand()
    {
        flashlight.SetActive(true);
        atesOnHand.SetActive(false);
        atesPreview.SetActive(false);
        laser.SetActive(false);
    }
}
