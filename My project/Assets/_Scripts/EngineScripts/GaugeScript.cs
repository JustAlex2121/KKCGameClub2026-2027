using UnityEngine;

public class GaugeScript : MonoBehaviour
{
    public Transform Dial;
    private Quaternion rotationalOffset;

    void Start()
    {
        //stores the difference between dial and this object on start
        //quaternions are combined using multiplication
        rotationalOffset = Quaternion.Inverse(Dial.rotation) * transform.rotation;

    }

    void Update()
    {
        if (Dial != null)
        {
            //applies the dial's rotation combined with the initial offset
            transform.rotation = Dial.rotation * rotationalOffset;
        }
    }
}