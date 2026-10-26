using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    [SerializeField] private float wallHitRadius = 4f;
 
    [Tooltip("Ignore bumps slower than this, so barely brushing a wall stays quiet.")]
    [SerializeField] private float minImpactSpeed = 0.3f;
 
    [Tooltip("Minimum seconds between noises, so grinding along a wall doesn't spam.")]
    [SerializeField] private float cooldown = 0.5f;
 
    private float nextAllowedTime;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time < nextAllowedTime) return;
        if (collision.relativeVelocity.magnitude < minImpactSpeed) return;
 
        
        ContactPoint contact = collision.GetContact(0);
        if (Mathf.Abs(contact.normal.y) > 0.5f) return;
 
        nextAllowedTime = Time.time + cooldown;
        NoiseSystem.Emit(contact.point, wallHitRadius);
    }
 
    
    public void Emit(float radius) => NoiseSystem.Emit(transform.position, radius);
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, wallHitRadius);
    }
}
