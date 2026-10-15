using System.Collections;
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
    [SerializeField] private Transform ontargetlocation;
    [SerializeField] private Transform offtargetlocation;

    [SerializeField] private float rodduration;
    private Vector3 origleverPosition;


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
        if(rodduration < 0)
        {
            rodduration *= -1;
        }

        if(transform.localPosition.y > 0f)
        {

            origleverPosition.y = maxLocalY;
            StartCoroutine(MoveRodGradually(false));

        }
        else if (transform.localPosition.y < 0f)
        {
            
            origleverPosition.y = minLocalY;
            StartCoroutine(MoveRodGradually(true));
        }

        //moves lever back to starting position when the player lets go of the mouse button
        transform.localPosition = origleverPosition;

    }

    private IEnumerator MoveRodGradually(bool goingdown)
    {
        Vector3 startPosition = rodtransform.position;
        float elapsedTime = 0f;

        if (goingdown)
        {
            while (elapsedTime < rodduration)
            {
                //moves the object each frame based on the amount of time elapsed and time left.
                rodtransform.position = Vector3.Lerp(startPosition, offtargetlocation.position, elapsedTime / rodduration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (elapsedTime < rodduration)
            {
                //moves the object each frame based on the amount of time elapsed and time left.
                rodtransform.position = Vector3.Lerp(startPosition, ontargetlocation.position, elapsedTime / rodduration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

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
            clampedY = Mathf.Clamp(targetLocalPos.y, minLocalY, maxLocalY);


            //applies clamped local position back to the object
            transform.localPosition = new Vector3(origleverPosition.x, clampedY, origleverPosition.z);
        }

    }
}
