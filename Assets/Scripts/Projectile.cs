using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15;
    public float lifetime = 3f;
    [Space]
    public Transform display;
    public float radius = 0.35f;
    public Vector3 scaleAxis = new Vector3(0, 0, 1);
    [Space]
    public GameObject impactEffect;

    private float t = 0;
    private Vector3 direction;

    private Vector3 lastPos, velocity;
    private float mag;
    private RaycastHit hit;

    public void Send(Vector3 from, Vector3 dir) {
        t = 0;
        transform.position = from;
        direction = dir;
        transform.rotation = Quaternion.LookRotation(dir);

        velocity = Vector3.zero;
        lastPos = from;

        if (display != null) {
            display.localScale = Vector3.zero;
            display.localPosition = Vector3.zero;
        }
    }

    void Update() {
        t += Time.deltaTime;
        if (t > lifetime) Destroy(gameObject);

        transform.position += direction * speed * 100 * Time.deltaTime;
        velocity = transform.position - lastPos;
        mag = velocity.magnitude;

        if (Physics.Raycast(lastPos, velocity, out hit, mag)) {
            if (impactEffect != null) Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gameObject);
        }

        if (display != null) {
            display.localScale = (Vector3.one * radius) + (scaleAxis * mag - scaleAxis * radius);
            display.localPosition = -Vector3.forward * mag / 2;
        }

        lastPos = transform.position;
    }
}
