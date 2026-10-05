using UnityEngine;
using UnityEngine.UIElements;

public class Levernew : MonoBehaviour
{
    public float minLocalX = -5f;
    public float maxLocalX = 5f;
    public float minLocalZ = -5f;
    public float maxLocalZ = 5f;
    public float mechspeedmultiplier;
    private float clampedX;
    private float clampedZ;

    private Camera mainCamera;
    private Vector3 dragOffset;
    private Plane movementPlane;
    private Transform parentTransform;
    [SerializeField] private Rigidbody mechrb;
    private Vector3 origleverPosition;
    private bool rotatemech;
    private bool movemech;

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
        movementPlane = new Plane(Vector3.up, transform.position);

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
        //moves lever back to starting position when the player lets go of the mouse button
        transform.localPosition = origleverPosition;

        rotatemech = false;
        //stops the mech on the map from being able to rotate on the y axis
        mechrb.constraints |= RigidbodyConstraints.FreezeRotationY;

        movemech = false;
        //stops the mech on the map from being able to move on the X and Y axis
        mechrb.constraints |= RigidbodyConstraints.FreezePositionX;
        mechrb.constraints |= RigidbodyConstraints.FreezePositionZ;

        //sets the mech's velocity to zero when the lever is no longer being held
        mechrb.linearVelocity = Vector3.zero;
        mechrb.angularVelocity = new Vector3(0, 0f, 0f);

    }

    void FixedUpdate()
    {
        if (movemech)
        {
            //vertical lever
            if (clampedX > 0)
            {
                Vector3 localDirection = new Vector3(0f, 0f, 1f);
                Vector3 globalDirection = mechrb.rotation * localDirection;
                Vector3 targetVelocity = globalDirection * mechspeedmultiplier;
                mechrb.linearVelocity = targetVelocity;

            }
            else if (clampedX < 0)
            {
                Vector3 localDirection = new Vector3(0f, 0f, -1f);
                Vector3 globalDirection = mechrb.rotation * localDirection;
                Vector3 targetVelocity = globalDirection * mechspeedmultiplier;
                mechrb.linearVelocity = targetVelocity;
            }

        }

        if (rotatemech) 
        {
            //horizontal lever
            if(clampedZ > 0)
            {
                mechrb.angularVelocity = new Vector3(0, -3f, 0f);
            }
            else if (clampedZ < 0)
            {
                mechrb.angularVelocity = new Vector3(0, 3f, 0f);
            }
           
            Debug.Log(mechrb.angularVelocity.magnitude);
        }
    }
 

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
            clampedX = Mathf.Clamp(targetLocalPos.x, minLocalX, maxLocalX);
            clampedZ = Mathf.Clamp(targetLocalPos.z, minLocalZ, maxLocalZ);

            //keeps Y position tracking the local origin or parent structure
            float keptY = targetLocalPos.y;

            //applies clamped local position back to the object
            transform.localPosition = new Vector3(clampedX, keptY, clampedZ);
        }


        
        if (minLocalX < 0 || maxLocalX > 0)
        {
            movemech = true;
            //allows the mech on the map to move on the X and Z axis
            mechrb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            mechrb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
       
        else if (minLocalZ < 0 || maxLocalZ > 0)
        {

            rotatemech = true;
            //allows for mech on map to rotate on the y axis
            mechrb.constraints &= ~RigidbodyConstraints.FreezeRotationY;

        }


    }
}
