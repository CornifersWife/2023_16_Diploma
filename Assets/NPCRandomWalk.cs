using UnityEngine;
using UnityEngine.AI;

public class NPCRandomWalk : MonoBehaviour, IWalkable {
    [Space(10)]
    [Header("Ranges")]
    [Range(-100, 100)]
    [SerializeField] private int xLocalRange;
    [Range(-100, 100)]
    [SerializeField] private int xGlobalRange;
    [Space(20)]
    [Range(-100, 100)]
    [SerializeField] private int zLocalRange;
    [Range(-100, 100)]
    [SerializeField] private int zGlobalRange;
    
    private NavMeshAgent navMeshAgent;
    private bool waypointSet = false;
    private Vector3 destination;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        Walk(destination);
    }

    public void Walk(Vector3 target) {
        if (waypointSet) {
            navMeshAgent.SetDestination(target);
        }

        if (Vector3.Distance(transform.position, target) < 0.1f) {
            waypointSet = false;
        }

        if (!waypointSet) {
            SetTargetPoint();
        }
    }

    public void SetTargetPoint() {
        float x = Random.Range(-xLocalRange, xLocalRange);
        float z = Random.Range(-zLocalRange, zLocalRange);

        Vector3 position = transform.position;
        destination = new Vector3(position.x + x, position.y, position.z + z);

        destination.x = Validate(destination.x, xGlobalRange);
        destination.z = Validate(destination.z, zGlobalRange);
        
        waypointSet = true;
    }

    private float Validate(float coord, float range) {
        if (coord > range)
            return range;
        if (coord < -range)
            return -range;
        return coord;
    }
}
