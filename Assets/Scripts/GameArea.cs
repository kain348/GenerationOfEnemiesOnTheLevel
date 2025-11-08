using UnityEngine;

[RequireComponent (typeof(Collider))]
[AddComponentMenu("Game/Area/Game Area")]
public class GameArea : MonoBehaviour
{
    [Header("Area Settings")]
    [SerializeField] private bool _drawGizmos = true;
    [SerializeField] private Color _gizmoColor = new Color(0, 1, 0, 0.1f);

    private Collider _areaCollider;

    private void Awake()
    {
        _areaCollider = GetComponent<Collider>();
        _areaCollider.isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IExitableFromArea exitable = other.GetComponent<IExitableFromArea>();
        exitable?.OnExitArea(this);
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        if (_areaCollider == null)
            _areaCollider = GetComponent<Collider>();

        Gizmos.color = _gizmoColor;

        if (_areaCollider is BoxCollider boxCollider)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(boxCollider.center, boxCollider.size);
        }
        else if (_areaCollider is SphereCollider sphereCollider)
        {
            Gizmos.DrawSphere(transform.position + sphereCollider.center, sphereCollider.radius);
        }
    }
}