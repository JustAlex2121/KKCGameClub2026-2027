using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public string targetTag = "Lever";

    public float transitionSpeed = 5f;

    public Vector3 cameraOffset = new Vector3(0f, 0f, 0f);

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform currentTarget;
    public bool isLocked = false;
    public bool returnPos = false;
    private bool storedPos;
    private bool ranroutine;
    public Image reticle;
    public MeshRenderer playerrender;
    public Collider playercollider;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        if (Input.GetKeyDown(KeyCode.F) && isLocked)
        {
            if (ranroutine)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isLocked = false;
                currentTarget = null;
                returnPos = true;
                ranroutine = false;
                reticle.enabled = true;
                playerrender.enabled = true;
                playercollider.enabled = true;
                MoveCamera();
            }
        }


    }

    void HandleClick()
    {


        //shoots a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //checks if the clicked object has the correct tag
            if (hit.collider.CompareTag(targetTag))
            {
                currentTarget = hit.transform;
                isLocked = true;
                if (!storedPos)
                {
                    originalPosition = transform.position;
                    originalRotation = transform.rotation;
                    storedPos = true;
                    MoveCamera();
                }

            }
        }
    }

    void MoveCamera()
    {
        if (isLocked && currentTarget != null)
        {
            if(!ranroutine)
            {
                StartCoroutine(MoveAndLookDownRoutine());
            }

        }
        else if (returnPos)
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;

            returnPos = false;
            storedPos = false;

        }
        
    }

    IEnumerator MoveAndLookDownRoutine()
    {
        reticle.enabled = false;
        playerrender.enabled = false;
        playercollider.enabled = false;

        Quaternion targetRotation = Quaternion.Euler(90f, 90f, 0);
        Vector3 targetPosition = currentTarget.position + cameraOffset;

        //continues until position and rotation are close enough to the target
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f ||
               Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            //moves camera towards target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);

            //rotates camera to look down
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

            //waits until the next frame
            yield return null;
        }

        //snaps exactly to final values to prevent floating point drift(prevents visual jitter or choppy movement)
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ranroutine = true;
    }
}