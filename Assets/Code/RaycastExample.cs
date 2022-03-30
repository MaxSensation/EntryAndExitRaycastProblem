// created by Maximiliam Rosén

using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var ray = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(ray, out var entryHit, float.MaxValue)) return;
        var exitPoint = entryHit.collider.GetExitPosition(entryHit.point, ray.direction, 1000f);

        // --------------------------------------------------------------------------------------------- Ignore this ---------------------------------------------------------------------------------------------
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(entryHit.point, 0.1f);
        Gizmos.DrawWireSphere(exitPoint, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, entryHit.point);
        Gizmos.DrawLine(new Ray(entryHit.point, ray.direction).GetPoint(1000f), exitPoint);
        // --------------------------------------------------------------------------------------------- Ignore this ---------------------------------------------------------------------------------------------
    }
}

// The implementation
internal static class ColliderExtension
{
    public static Vector3 GetExitPosition(this Collider collider, Vector3 entryPoint, Vector3 direction, float checkDistance) => GetExitPosition(collider, new Ray(entryPoint, direction), checkDistance);
    public static Vector3 GetExitPosition(this Collider collider, Ray ray, float checkDistance)
    {
        // Get a point x distance from the entryPoint
        ray.origin = ray.GetPoint(checkDistance);
        // Reverse the ray direction
        ray.direction = -ray.direction;
        // Call the raycast from the collider
        collider.Raycast(ray, out var hit, float.MaxValue);
        return hit.point;
    }
}