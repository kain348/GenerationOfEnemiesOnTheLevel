using UnityEngine;

[AddComponentMenu("Game/Spawners/Spawn Point")]
public class SpawnPoint : MonoBehaviour
{
    public Vector3 GetPoint() => transform.position;
    
    public Quaternion GetRotation() => transform.rotation;

    public Vector3 GetDirection() => Random.onUnitSphere.normalized;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}