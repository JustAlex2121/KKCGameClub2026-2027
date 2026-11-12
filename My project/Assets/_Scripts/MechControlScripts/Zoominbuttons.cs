using UnityEngine;

public class Zoominbuttons : MonoBehaviour
{
    public bool isPlusButton;
    public bool isMinusButton;

    [SerializeField] private Camera mapcamera;


    [SerializeField] private static int currentFOV;

    [Header("Lower values zoom in further")]
    [Range(26, 46)] public int maxZoomValue;
    [Range(48, 56)] public int minZoomValue;
   

    private void Start()
    {
        currentFOV = minZoomValue;
        mapcamera.fieldOfView = currentFOV;
    }
    private void OnMouseDown()
    {
        if (isPlusButton)
        {
            Debug.Log("plus button pressed");
            if (currentFOV > maxZoomValue)
            {
                currentFOV -= 2;
                mapcamera.fieldOfView = currentFOV;
            }
        }
        else if (isMinusButton)
        {
            Debug.Log("minus button pressed");
            if (currentFOV < minZoomValue)
            {
                currentFOV += 2;
                mapcamera.fieldOfView = currentFOV;
            }
        }
        else
        {
            Debug.Log("Assign a button type to use");

        }
    }
}
