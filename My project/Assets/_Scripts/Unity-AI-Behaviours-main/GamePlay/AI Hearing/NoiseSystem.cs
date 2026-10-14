using UnityEngine;
using System;
public class NoiseSystem : MonoBehaviour
{
    /// <summary>position of the noise, radius it can be heard from</summary>
    public static event Action<Vector3, float> NoiseMade;
 
    // Static events survive between play sessions if "Enter Play Mode Options" is on, so clear them.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics() => NoiseMade = null;
 
    public static void Emit(Vector3 position, float radius)
    {
        NoiseMade?.Invoke(position, radius);
    }
}
