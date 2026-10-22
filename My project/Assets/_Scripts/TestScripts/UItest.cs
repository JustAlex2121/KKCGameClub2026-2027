using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UItest : MonoBehaviour
{

    public GameObject enginedial;
    [SerializeField] private Image testfill;
    [SerializeField] private GameObject engineonimage1;
    [SerializeField] private GameObject engineonimage2;
    [SerializeField] private GameObject engineonimage3;
    [SerializeField] private GameObject engineonimage4;
    
    private float inspectorz;
    public float fillamount;
    /*
    public bool enginerod1on = false;
    public bool enginerod2on = false;
    public bool enginerod3on = false;
    public bool enginerod4on = false;
    */
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

        
    }

    /*
     public void CheckEngineRods()
    {
        if (enginerod1on)
        {
            engineonimage1.SetActive(true);
        }
        else
        {
            engineonimage1.SetActive(false);
        }

        if (enginerod2on)
        {
            engineonimage2.SetActive(true);
        }
        else
        {
            engineonimage2.SetActive(false);
        }

        if (enginerod3on)
        {
            engineonimage3.SetActive(true);
        }
        else
        {
            engineonimage3.SetActive(false);
        }

        if (enginerod4on)
        {
            engineonimage4.SetActive(true);
        }
        else
        {
            engineonimage4.SetActive(false);
        }
    }
    */

}
