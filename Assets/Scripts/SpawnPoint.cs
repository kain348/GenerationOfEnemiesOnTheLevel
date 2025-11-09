using UnityEngine;

[AddComponentMenu("Game/Spawners/Spawn Point")]
public class SpawnPoint : MonoBehaviour
{
    public Vector3 GetPoint()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public Vector3 GetDirection()
    {
        return Random.onUnitSphere.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}