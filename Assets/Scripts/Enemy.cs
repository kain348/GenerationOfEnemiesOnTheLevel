using UnityEngine;
using System;

[SelectionBase]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
[AddComponentMenu("Game/Enemies/Enemy")]
public class Enemy : MonoBehaviour, IExitableFromArea
{
    private Mover _mover;
    private Collider _collider;
    private Renderer _renderer;

    public event Action<Enemy> ExitedFromArea;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    public void Reset()
    {
        _mover.Stop();
        transform.rotation = Quaternion.identity;

        //_rigidbody.velocity = Vector3.zero;
        //_rigidbody.angularVelocity = Vector3.zero;

    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero && direction == null)
            throw new ArgumentException(nameof(direction));

        _mover.Move(direction);

        //_directionMovement = directionMovement.normalized;
        //_isMove = true;
    }

    public void OnExitArea(GameArea area)
    {
        ExitedFromArea?.Invoke(this);
    }
}