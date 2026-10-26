using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class NoiseHearingAI : MonoBehaviour
{
    private enum State { Wander, Investigate, Search }
 
    [SerializeField] private NavAgentMover mover = null;
 
    [Header("Hearing")]
    [Tooltip("1 = normal. Bigger = better ears (hears quieter/farther noises).")]
    [SerializeField] private float hearingMultiplier = 1f;
    [Tooltip("Max error (metres) in where the enemy thinks the noise came from.")]
    [SerializeField] private float noiseInaccuracy = 3f;
 
    [Header("Behaviour")]
    [SerializeField] private float searchDuration = 4f;
    [SerializeField] private float wanderRadius = 12f;
    [SerializeField] private Vector2 wanderPause = new Vector2(2f, 5f);

    [Header("Catching the mech")]
    [SerializeField] private Rigidbody mechRb = null;
    [SerializeField] private string gameOverScene = "GameOver";
 
    private State state = State.Wander;
    private float currentNoiseScore;   // how "strong" the noise we're investigating was (0-1)
    private float stateTimer;
    private Vector3 investigatePos;
    private bool caught;
 
    private void Awake()
    {
        if (!mover) mover = GetComponent<NavAgentMover>();
    }
 
    private void OnEnable() => NoiseSystem.NoiseMade += OnNoiseHeard;
    private void OnDisable() => NoiseSystem.NoiseMade -= OnNoiseHeard;
 
    private void Start() => EnterWander();
 
    private void Update()
    {
        switch (state)
        {
            case State.Wander:
                if (mover.HasArrived && Time.time >= stateTimer)
                {
                    mover.MoveToPos(RandomNavPoint(transform.position, wanderRadius));
                    stateTimer = Time.time + Random.Range(wanderPause.x, wanderPause.y);
                }
                break;
 
            case State.Investigate:
                if (mover.HasArrived)
                {
                    state = State.Search;
                    stateTimer = Time.time + searchDuration;
                }
                break;
 
            case State.Search:
                // Poke around the spot a bit, then give up.
                if (mover.HasArrived)
                    mover.MoveToPos(RandomNavPoint(investigatePos, noiseInaccuracy * 2f));
                if (Time.time >= stateTimer)
                    EnterWander();
                break;
        }
    }
 
    private void OnNoiseHeard(Vector3 pos, float radius)
    {
        float dist = Vector3.Distance(transform.position, pos);
        float range = radius * hearingMultiplier;
        if (dist > range) return;
        
 
        // 1 = right on top of us, 0 = at the edge of hearing range
        float score = 1f - (dist / range);
 
        // Already chasing a louder/closer noise? Ignore this one.
        if (state == State.Investigate && score <= currentNoiseScore) return;
 
        currentNoiseScore = score;
        investigatePos = RandomNavPoint(pos, noiseInaccuracy);
        state = State.Investigate;
        mover.MoveToPos(investigatePos);
    }
 
    private void EnterWander()
    {
        state = State.Wander;
        currentNoiseScore = 0f;
        stateTimer = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (caught) return;

        if (other.attachedRigidbody == mechRb)
        {
            caught = true;
            SceneManager.LoadScene(gameOverScene);
        }
    }
 
    
    private static Vector3 RandomNavPoint(Vector3 center, float radius)
    {
        Vector3 p = center + Random.insideUnitSphere * radius;
        p.y = center.y;
        return UnityEngine.AI.NavMesh.SamplePosition(p, out UnityEngine.AI.NavMeshHit hit, radius + 2f, UnityEngine.AI.NavMesh.AllAreas) ? hit.position : center;
    }
 
    private void OnDrawGizmosSelected()
    {
        if (state != State.Wander)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(investigatePos, .4f);
        }
    }
}
