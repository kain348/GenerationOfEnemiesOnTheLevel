using UnityEngine;
using System;

[SelectionBase]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
[AddComponentMenu("Game/Enemies/Enemy")]
public class Enemy : MonoBehaviour, IExitableFromArea
{
    private Mover _mover;
    private Collider _collider;

    public event Action<Enemy> ExitedFromArea;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collider = GetComponent<Collider>();
    }

    public void Reset()
    {
        _mover.Stop();
        transform.rotation = Quaternion.identity;
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero && direction == null)
            throw new ArgumentException(nameof(direction));

        _mover.Move(direction);
    }

    public void OnExitArea(GameArea area)
    {
        ExitedFromArea?.Invoke(this);
    }
}