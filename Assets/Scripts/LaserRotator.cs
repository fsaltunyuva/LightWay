using UnityEngine;

public class LaserRotator : MonoBehaviour
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private PlayerMovement playerMovement;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction;
        if(playerMovement.amIFacingLeft)
        {
            direction = -(mousePosition - pivotTransform.position);
        }
        else
        {
            direction = mousePosition - pivotTransform.position;
        }

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}