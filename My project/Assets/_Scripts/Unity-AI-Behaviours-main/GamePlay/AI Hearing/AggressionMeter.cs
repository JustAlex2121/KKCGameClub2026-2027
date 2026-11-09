using UnityEngine;

public class AggressionMeter : MonoBehaviour
{
    
    [Range(0, 100)]
    [SerializeField] private float aggression = 0f;
    [SerializeField] private float gainpernoise = 25f;

    

    private void BillTol(Vector3 pos, float radius)
    {
        aggression += gainpernoise;
        aggression = Mathf.Clamp(aggression, 0f, 100f);
    }
    
   
}
