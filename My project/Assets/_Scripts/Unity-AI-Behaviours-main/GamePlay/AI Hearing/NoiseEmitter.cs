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
 
        // Only count walls: contact normal pointing sideways, not up/down (floor).
        ContactPoint contact = collision.GetContact(0);
        if (Mathf.Abs(contact.normal.y) > 0.5f) return;
 
        nextAllowedTime = Time.time + cooldown;
        Debug.Log($"wall hit at {contact.point}, noise radius {wallHitRadius}");
        NoiseSystem.Emit(contact.point, wallHitRadius);
    }
 
    /// <summary>For any other one-off noise you add later.</summary>
    public void Emit(float radius) => NoiseSystem.Emit(transform.position, radius);
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, wallHitRadius);
    }
}
