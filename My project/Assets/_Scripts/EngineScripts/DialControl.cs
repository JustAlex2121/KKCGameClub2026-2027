using UnityEngine;


public class DialControl : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.forward; // Axis the dial rotates on
    private Camera mainCamera;

    private float baseAngle;
    private float currentAngle = 0f;

    private Quaternion startingRotation;

    [Header("Allowed snap positions (relative to start)")]
    [SerializeField] private bool useSnapAngles = false;
    [SerializeField] private float[] snapAngles = new float[] { -30f, 0f, 15f, 60f, 85f };

    [Header("Use if not using Snap Angles")]
    [SerializeField] private bool useLimits = false;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 270f;
    [SerializeField] private float sensitivity = 1.0f;

    [SerializeField] private GaugeScript gauge;



    void Start()
    {
        mainCamera = Camera.main;

        startingRotation = transform.localRotation;


        //current angle matching the dial's starting local rotation
        currentAngle = Vector3.Dot(transform.localEulerAngles, rotationAxis);
    }
    void OnMouseDown()
    {
        

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == gameObject.transform)
            {
                //calculates the initial angular offset between the mouse's position and the dial's center
                baseAngle = GetMouseAngle();
                //base angle is different from current angle because current angle is set at the beginning where the dial starts but base angle is where the player's mouse was at the start of the frame they interacted with the dial relative to the dial's center
            }
        }
    }


    void OnMouseDrag()
    {
        float currentMouseAngle = GetMouseAngle();

        //calculates how much the mouse rotated around the dial center by calculating the shortest distance between the two angles * the sensitivity(sensitivity exagerates the angle making the dial turn faster)
        float angleDifference = Mathf.DeltaAngle(baseAngle, currentMouseAngle) * sensitivity;


        float targetAngle = currentAngle + angleDifference;

        if (useLimits)
        {
            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
        }

        if (useSnapAngles)
        {
            float snappedAngle = GetClosestAllowedAngle(targetAngle);

            //rotates object to specifc set angle on the specified axis while taking into account the starting rotation
            transform.localRotation = startingRotation * Quaternion.AngleAxis(snappedAngle, rotationAxis);
        }
        else
        {
            //applies rotation around specified axis
            transform.localRotation = Quaternion.AngleAxis(targetAngle, rotationAxis);
        }

        //tracks state for next frame
        //the current angle becomes the target angle because at this point the dial should have moved to where the target angle was.
        currentAngle = targetAngle;

        //at this point the base angle becomes where the mouse was relative to the center of the dial on the last frame
        baseAngle = currentMouseAngle;

    }

    private float GetClosestAllowedAngle(float targetAngle)
    {
        if (snapAngles == null || snapAngles.Length == 0)
        {
            //returns targetAngle if list is empty
            return targetAngle;
        }

        float closest = snapAngles[0];
        gauge.selectedValue = gauge.gaugeValues[0];
        float minDistance = Mathf.Abs(targetAngle - closest);

        for (int i = 1; i < snapAngles.Length; i++)
        {
            float distance = Mathf.Abs(targetAngle - snapAngles[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = snapAngles[i];
                gauge.selectedValue = gauge.gaugeValues[i];
            }
        }

        return closest;
    }




    //math to get the current 2D screen angle of the mouse relative to the center of the dial
    private float GetMouseAngle()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 currentMousePos = Input.mousePosition;
        currentMousePos.z = 0;

        Vector3 direction = currentMousePos - screenPos;

        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}