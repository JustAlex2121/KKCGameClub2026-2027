using UnityEngine;

public class AggressionMeter : MonoBehaviour
{
    public enum AggressionLevel
    {
        Stationary,
        Curious,
        Hunting
    }

    [Range(0, 100)]
    [SerializeField] private float aggression = 0f;
    [SerializeField] private float gainpernoise = 25f;
    [SerializeField] private float drainpersecond = 3f;
    

    private void OnEnable() => NoiseSystem.NoiseMade += BillTol;
    private void OnDisable() => NoiseSystem.NoiseMade -= BillTol;

    

    private void BillTol(Vector3 pos, float radius)
    {
        aggression += gainpernoise;
        aggression = Mathf.Clamp(aggression, 0f, 100f);
    }

    public AggressionLevel GetLevel()
    {
        if (aggression <= 25)
        {
            return AggressionLevel.Stationary;
        }
        else if (aggression <=75)
        {
            return AggressionLevel.Curious;
        }

        return AggressionLevel.Hunting;
    }

    private void Update()
    {
        aggression -= drainpersecond * Time.deltaTime;
        aggression = Mathf.Clamp(aggression, 0f, 100f);
    }
    
   
}
