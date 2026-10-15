using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UItest : MonoBehaviour
{

    public GameObject enginedial;
    [SerializeField] private Image testfill;
    [SerializeField] private GameObject engineonimage;
    private float inspectorz;
    public float fillamount;
    public bool enginerodon = false;

    private void Update()
    {
        inspectorz = enginedial.transform.eulerAngles.z;

        if (inspectorz > 180f)
        {
            inspectorz -= 360f;
        }


        fillamount = inspectorz / 180f;

        if (fillamount < 0)
        {
            fillamount = fillamount * -1;
        }
        testfill.fillAmount = fillamount;

        if(enginerodon)
        {
            engineonimage.SetActive(true);
        }
        else
        {
            engineonimage.SetActive(false);
        }
    }

}
