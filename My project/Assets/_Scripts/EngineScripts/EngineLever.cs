using UnityEngine;

public class EngineLever : MonoBehaviour
{
    public float minLocalY = -5f;
    public float maxLocalY = 5f;


    private float clampedY;

    private Camera mainCamera;
    private Vector3 dragOffset;
    private Plane movementPlane;
    private Transform parentTransform;
    [SerializeField] private Transform rodtransform;
    private Vector3 origleverPosition;

    private bool moverod;

    void Start()
    {
        origleverPosition = transform.localPosition;
        mainCamera = Camera.main;

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
        /*
        if(transform.localPosition.y == minLocalY || transform.localPosition.y == maxLocalY)
        {
            //alt method
            origleverPosition = transform.localPosition;
        }
        */
        if(transform.localPosition.y > 0f)
        {
            origleverPosition.y = maxLocalY;
        }
        else if (transform.localPosition.y < 0f)
        {
            origleverPosition.y = minLocalY;
        }

        //moves lever back to starting position when the player lets go of the mouse button
        transform.localPosition = origleverPosition;

    }
    /*
    void FixedUpdate()
    {
       //movement code for rod goes here
    }
    */


    void OnMouseDrag()
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
            clampedY = Mathf.Clamp(targetLocalPos.y, minLocalY, maxLocalY);


            //applies clamped local position back to the object
            transform.localPosition = new Vector3(origleverPosition.x, clampedY, origleverPosition.z);
        }

    }
}
