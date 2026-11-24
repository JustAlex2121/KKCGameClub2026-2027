using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ConsoleLevernew : MonoBehaviour
{
    public float minLocalX = -5f;
    public float maxLocalX = 5f;
    public float midLocalX = 0f;
 
    private float clampedX;
    private float clampedZ;

    private Camera mainCamera;
    private CameraController camcontrol;
    private Vector3 dragOffset;
    private Plane movementPlane;
    private Transform parentTransform;

    private bool rotatemech;
    private bool movemech;

    public bool isleftLever;


    private Vector3 origleverPosition;

    public bool isUp;
    public bool isNeutral;
    [SerializeField] private OnOffButton OnOff;


    void Start()
    {
        isNeutral = true;
        origleverPosition = transform.localPosition;
        mainCamera = Camera.main;
        camcontrol = mainCamera.GetComponent<CameraController>();
        //grabs the transform of the parent of the object this script is attached to
        parentTransform = transform.parent;
    }


    void OnMouseDown()
    {


            //creates a horizontal plane at the object's current height
            movementPlane = new Plane(Vector3.forward, transform.position);

            //finds the exact hit point of the mouse on that plane
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (movementPlane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPos = ray.GetPoint(distance);

                //calculates offset so the object doesnt snap its center to the mouse
                dragOffset = transform.position - mouseWorldPos;
            }


    }



    void OnMouseUp()
    {

        origleverPosition.x = GetClosestNumber(transform.localPosition.x, new float[] { maxLocalX, minLocalX, midLocalX });
        if (origleverPosition.x == maxLocalX)
        {
            isUp = true;
            isNeutral = false;
        }
        else if(origleverPosition.x == minLocalX)
        {
            isUp = false;
            isNeutral = false;
        }
        else if(origleverPosition.x == midLocalX)
        {
            isUp = false;
            isNeutral = true;
        }

        //moves lever back to starting position when the player lets go of the mouse button
        transform.localPosition = origleverPosition;

    }

    public void EngineOffBehavior()
    {
        origleverPosition.x = midLocalX;
        isUp = false;
        isNeutral = true;
        transform.localPosition = origleverPosition;
    }

  
    public float GetClosestNumber(float target, float[] numbers)
    {
          if (numbers == null || numbers.Length == 0)
          {
                Debug.LogWarning("Number array is empty!");
                return target;
          }

          float closest = numbers[0];
          float smallestDifference = Mathf.Abs(target - closest);

          for (int i = 1; i < numbers.Length; i++)
          {
                float currentDifference = Mathf.Abs(target - numbers[i]);

                if (currentDifference < smallestDifference)
                {
                    smallestDifference = currentDifference;
                    closest = numbers[i];
                }
          }

          return closest;
    }
    
    
    
    void OnMouseDrag()
    {
        if (OnOff.isOn)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (movementPlane.Raycast(ray, out float distance))
            {
                Vector3 targetWorldPos = ray.GetPoint(distance) + dragOffset;
                Vector3 targetLocalPos;

                //converts target position from world space to local space(solved a problem where using world space would make the lever move to different coordinates when interacted with)
                if (parentTransform != null)
                {
                    targetLocalPos = parentTransform.InverseTransformPoint(targetWorldPos);
                }
                else
                {
                    //if there's no parent then local space equals world space
                    targetLocalPos = targetWorldPos;
                }

                //clamps the local coordinates
                clampedX = Mathf.Clamp(targetLocalPos.x, minLocalX, maxLocalX);


                //applies clamped local position back to the object
                transform.localPosition = new Vector3(clampedX, origleverPosition.y, origleverPosition.z);
            }
        }
       

    }

    public void OnMoveLeftLeverUp(InputAction.CallbackContext context)
    {
        if (isleftLever && OnOff.isOn)
        {
            if (camcontrol.lookingatConsole && !isUp)
            {
                if (isNeutral)
                {
                    isNeutral = false;
                    isUp = true;
                    //origleverPosition.x = maxLocalX;

                }
                if (!isNeutral)
                {
                    isUp = false;
                    isNeutral = true;
                    //origleverPosition.x = midLocalX;
                }
                HandleMovement();
                //transform.localPosition = origleverPosition;
            }
            else
            {
                Debug.Log("lever is all the way up");
            }
        }
        
    }

    public void OnMoveLeftLeverDown(InputAction.CallbackContext context)
    {
        if (isleftLever && OnOff.isOn)        
        {
            if (camcontrol.lookingatConsole && (isUp || isNeutral))
            {

                if (isUp)
                {
                    isUp = false;
                    isNeutral = true;
                   // origleverPosition.x = midLocalX;

                }
                if (isNeutral)
                {
                    isUp = false;
                    isNeutral = false;
                   // origleverPosition.x = minLocalX;
                }
                HandleMovement();
                //transform.localPosition = origleverPosition;
            }
            else
            {
                Debug.Log("lever is all the way down");
            }
        }

    }

    public void OnMoveRightLeverUp(InputAction.CallbackContext context)
    {
        if (!isleftLever && OnOff.isOn)
        {
            if (camcontrol.lookingatConsole && !isUp)
            {
                if (isNeutral)
                {
                    isNeutral = false;
                    isUp = true;
                    //origleverPosition.x = maxLocalX;

                }
                if (!isNeutral)
                {
                    isUp = false;
                    isNeutral = true;
                   // origleverPosition.x = midLocalX;
                }
                HandleMovement();
                //transform.localPosition = origleverPosition;
            }
            else
            {
                Debug.Log("lever is all the way up");
            }
        }

    }

    public void OnMoveRightLeverDown(InputAction.CallbackContext context)
    {
        if (!isleftLever && OnOff.isOn)
        {
            if (camcontrol.lookingatConsole && (isUp || isNeutral))
            {

                if (isUp)
                {
                    isUp = false;
                    isNeutral = true;
                    //origleverPosition.x = midLocalX;

                }
                if (isNeutral)
                {
                    isUp = false;
                    isNeutral = false;
                    //origleverPosition.x = minLocalX;
                }
                HandleMovement();
                //transform.localPosition = origleverPosition;
            }
            else
            {
                Debug.Log("lever is all the way down");
            }
        }

    }

    private void HandleMovement()
    {
        if (isUp)
        {
            origleverPosition.x = maxLocalX;
        }
        if (isNeutral)
        {
            origleverPosition.x = midLocalX;

        }
        else
        {
            origleverPosition.x = minLocalX;
        }
        transform.localPosition = origleverPosition;
    }
}
