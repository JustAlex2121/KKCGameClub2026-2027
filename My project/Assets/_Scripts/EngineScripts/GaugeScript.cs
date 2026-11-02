using UnityEngine;

public class GaugeScript : MonoBehaviour
{
    public Transform Dial;
    private Quaternion rotationalOffset;
    public float[] gaugeValues = new float[] { 0f, 20f, 40f, 60f, 80f, 100f, 120f, 140f, 160f};
    public float selectedValue;

    void Start()
    {
        selectedValue = gaugeValues[0];
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