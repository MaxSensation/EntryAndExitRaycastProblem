// created by Maximiliam Rosén

using UnityEngine;
public class RaycastExample : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var debugBoxSize = Vector3.one * 0.1f;
        // The total distance to check for the collider
        const float distance = 5f;
        // Here we create the ray with the starting position and direction
        var ray = new Ray(transform.position, transform.forward);
        // If we hit anything continue
        if (!Physics.Raycast(ray, out var entryHit, distance)) return;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(ray.origin, debugBoxSize);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * entryHit.distance);
        // Here we create a new starting position from the opposite side of the collider
        ray.origin = ray.GetPoint(distance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(ray.origin, debugBoxSize);
        // here we we flip the direction so the raycast start from the opposite side of the collider and pointing inwards
        ray.direction = -ray.direction;
        // If we hit anything continue
        if (!entryHit.collider.Raycast(ray, out var exitHit, distance)) return;
        Gizmos.color = Color.red;
        // We now have the entryHit and the exitHit of the collider
        Gizmos.DrawCube(entryHit.point, debugBoxSize);
        Gizmos.DrawCube(exitHit.point, debugBoxSize);
        Gizmos.DrawLine(entryHit.point, exitHit.point);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(ray.origin, exitHit.point);
    }
}
