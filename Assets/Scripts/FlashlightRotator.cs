using System.Collections;
using UnityEngine;

public class FlashlightRotator : MonoBehaviour
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private SpriteRenderer flashlightSpriteRenderer;
    public bool isPlaced = false;
    public float fadeDuration = 1f; 
    [SerializeField] private PolygonCollider2D flashlightCollider;
    [SerializeField] private GameObject flashlightLightPrefab;
    private Vector3 oldFlashlightPosition;
    private Quaternion oldFlashlightRotation;
    private GameObject newFlashlight;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // isPlaced = true;
            // StartCoroutine(EnableDrop());
        }
        if (!isPlaced)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            Vector3 direction = mousePosition - pivotTransform.position;
        
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void placeFlashLight()
    {
        Debug.Log("Placing old flashlight");
        //flashlight.transform.position = pivotTransform.position;
        oldFlashlightPosition = pivotTransform.position;
        oldFlashlightRotation = transform.rotation;
        flashlightCollider.isTrigger = false; // To prevent flashlight from passing through walls
        isPlaced = true;
        StartCoroutine(EnableDrop());
    }

    IEnumerator EnableDrop()
    {
        flashlight.transform.SetParent(null);
        yield return new WaitForSeconds(3f);
        flashlight.AddComponent<Rigidbody2D>();
        StartCoroutine(FadeOutAndDestroy());
    }
    
    IEnumerator FadeOutAndDestroy()
    {
        Color color = flashlightSpriteRenderer.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            flashlightSpriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        Debug.Log("Destroying flashlight");
        Destroy(flashlight);
                
        flashlight = newFlashlight;
        flashlightCollider = newFlashlight.GetComponent<PolygonCollider2D>();
        flashlightSpriteRenderer = newFlashlight.GetComponent<SpriteRenderer>();
        Debug.Log("New flashlight light replaced the previous one");
    }

    public void InstantiateNewFlashlightLight()
    {
        Debug.Log("Creating new flashlight light");
        newFlashlight = Instantiate(flashlightLightPrefab, oldFlashlightPosition, oldFlashlightRotation, gameObject.transform);
    }

    public void SecondMethod()
    {
        Vector3 positionOfFlashlight = flashlight.transform.position;
        Quaternion worldRotation = flashlight.transform.rotation;
        Vector3 worldScale = flashlight.transform.lossyScale;
        
        newFlashlight = Instantiate(flashlight, positionOfFlashlight, worldRotation);
        newFlashlight.transform.localScale = worldScale; // Ölçek elle atanmalı çünkü lossyScale direkt atanamaz
        newFlashlight.transform.SetParent(null); // Child olmaktan çıkar
        
        newFlashlight.GetComponent<PolygonCollider2D>().isTrigger = false; // To prevent flashlight from passing through walls
        //isPlaced = true;
        StartCoroutine(EnableDrop2(newFlashlight));
    }
    
    IEnumerator EnableDrop2(GameObject newFlashlight)
    {
        newFlashlight.transform.SetParent(null);
        yield return new WaitForSeconds(3f);
        newFlashlight.AddComponent<Rigidbody2D>();
        StartCoroutine(FadeOutAndDestroy2(newFlashlight.GetComponent<SpriteRenderer>(), newFlashlight));
    }
    
    IEnumerator FadeOutAndDestroy2(SpriteRenderer flashlightSpriteRenderer, GameObject newFlashlight)
    {
        Color color = flashlightSpriteRenderer.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            flashlightSpriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        Destroy(newFlashlight);
    }
}