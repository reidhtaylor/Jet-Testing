using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public static MapBounds instance;

    public Vector3 size;
    public Vector3 offset;

    private void Awake() {
        instance = this;
    }

    public static float GetTop() => instance.offset.y + instance.size.y / 2;
    public static float GetBottom() => instance.offset.y - instance.size.y / 2;
    public static float GetLeft() => instance.offset.x - instance.size.x / 2;
    public static float GetRight() => instance.offset.x + instance.size.x / 2;
    public static float GetForward() => instance.offset.z + instance.size.z / 2;
    public static float GetBackward() => instance.offset.z - instance.size.z / 2;
    public static bool InBounds(Vector3 p) {
        if (p.y <= GetBottom() || p.y >= GetTop()) return false;
        if (p.x <= GetLeft() || p.x >= GetRight()) return false;
        if (p.z <= GetBackward() || p.z >= GetForward()) return false;
        return true;
    }
    public static Vector3 ToBounds(Vector3 p) => instance.offset - p;

    public static Vector3 RandomPoint() {
        return instance.offset + (Vector3.up * instance.size.y / 2 * Random.Range(-1f, 1f)) + (Vector3.right * instance.size.y / 2 * Random.Range(-1f, 1f)) + (Vector3.forward * instance.size.y / 2 * Random.Range(-1f, 1f));
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float sphereSize = 4;

        Gizmos.DrawWireCube(offset, size);

        Gizmos.DrawSphere(offset + Vector3.right * size.x / 2 + Vector3.forward * size.z / 2 + Vector3.up * size.y / 2, sphereSize);
        Gizmos.DrawSphere(offset + Vector3.right * size.x / 2 + Vector3.forward * size.z / 2 - Vector3.up * size.y / 2, sphereSize);

        Gizmos.DrawSphere(offset - Vector3.right * size.x / 2 + Vector3.forward * size.z / 2 + Vector3.up * size.y / 2, sphereSize);
        Gizmos.DrawSphere(offset - Vector3.right * size.x / 2 + Vector3.forward * size.z / 2 - Vector3.up * size.y / 2, sphereSize);

        Gizmos.DrawSphere(offset - Vector3.right * size.x / 2 - Vector3.forward * size.z / 2 + Vector3.up * size.y / 2, sphereSize);
        Gizmos.DrawSphere(offset - Vector3.right * size.x / 2 - Vector3.forward * size.z / 2 - Vector3.up * size.y / 2, sphereSize);

        Gizmos.DrawSphere(offset + Vector3.right * size.x / 2 - Vector3.forward * size.z / 2 + Vector3.up * size.y / 2, sphereSize);
        Gizmos.DrawSphere(offset + Vector3.right * size.x / 2 - Vector3.forward * size.z / 2 - Vector3.up * size.y / 2, sphereSize);
    }
}
