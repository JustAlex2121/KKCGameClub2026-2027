using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform mechtransform;
    [SerializeField] private float cameraoffset;
    private Vector3 targetposition;

    void Update()
    {
        if (mechtransform.position != targetposition)
        {
            targetposition = mechtransform.position;
            targetposition.y = cameraoffset;

            transform.position = targetposition;
        }
    }
}
