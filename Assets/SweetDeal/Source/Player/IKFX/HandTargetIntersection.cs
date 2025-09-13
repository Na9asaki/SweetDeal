using System;
using UnityEngine;

public class HandTargetIntersection : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    
    public event Action<Vector3> OnIntersect;
    public event Action OnRelease;

    public Vector3? Position { get; private set; }
    public Vector3? Normal { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        OnIntersect?.Invoke(other.ClosestPoint(endPoint.position));
        Position = (other.ClosestPoint(endPoint.position));
    }

    private void OnTriggerStay(Collider other)
    {
        Position = (other.ClosestPoint(endPoint.position));
        Normal = Position - endPoint.position;
    }

    private void OnTriggerExit(Collider other)
    {
        OnRelease?.Invoke();
        Position = null;
        Normal = null;
    }
}
