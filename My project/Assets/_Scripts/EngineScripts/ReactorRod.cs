using UnityEngine;

public class ReactorRod : MonoBehaviour
{
    public UItest ui;
    [SerializeField]private CheckButton button;

    [Range(1, 4)] public int rodNumber;



    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("trigger entered");
        if (trigger.CompareTag("On"))
        {
            if (rodNumber == 1)
            {
                button.enginerod1on = true;
            }
            else if (rodNumber == 2)
            {
                button.enginerod2on = true;
            }
            else if(rodNumber == 3)
            {
                button.enginerod3on = true;
            }
            else if(rodNumber == 4)
            {
                button.enginerod4on = true;
            }
            
        }
        else if (trigger.CompareTag("Off"))
        {
            if (rodNumber == 1)
            {
                button.enginerod1on = false;
            }
            else if (rodNumber == 2)
            {
                button.enginerod2on = false;
            }
            else if (rodNumber == 3)
            {
                button.enginerod3on = false;
            }
            else if (rodNumber == 4)
            {
                button.enginerod4on = false;
            }
        }
    }
}
