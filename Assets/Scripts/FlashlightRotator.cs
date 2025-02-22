using System.Collections;
using UnityEngine;

public class FlashlightRotator : MonoBehaviour
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private SpriteRenderer flashlightSpriteRenderer;
    public bool isPlaced = false;
    public float fadeDuration = 1f; 
    
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

        Destroy(flashlight);
    }
}