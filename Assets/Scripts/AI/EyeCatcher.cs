using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCatcher : MonoBehaviour
{
    [System.Serializable]
    public class ViewCatch {
        public Vector3 offset = new Vector3(0, 0, 0);
        public Vector3 dir = new Vector3(0, 0, 0);
        public float length = 3;
        [Space]
        [Range(-1, 1)]
        public float value = 0;
    }

    public Color col = Color.red;
    public ViewCatch[] catches = new ViewCatch[0];
    private Ray ray;

    private void Start() {
        ray = new Ray(Vector3.zero, Vector3.up);
    }

    public ViewCatch Process() {
        for (int i = 0; i < catches.Length; i++) {
            ray.origin = transform.TransformPoint(catches[i].offset);
            ray.direction = transform.TransformDirection(catches[i].dir);
            if (Physics.Raycast(ray, catches[i].length)) {
                return catches[i];
            }
        }
        return null;
    }

    public void OnDrawGizmosSelected() {
        if (!enabled) return;
        Gizmos.color = col;
        for (int i = 0; i < catches.Length; i++) {
            Gizmos.DrawSphere(transform.TransformPoint(catches[i].offset), 0.2f);
            Gizmos.DrawLine(transform.TransformPoint(catches[i].offset), transform.TransformPoint(catches[i].offset) + transform.TransformDirection(catches[i].dir) * catches[i].length);
        }
    }
}
