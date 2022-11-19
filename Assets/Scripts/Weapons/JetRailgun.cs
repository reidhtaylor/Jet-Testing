using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetRailgun : JetWeapon
{
    public Transform[] firepoints;
    public Projectile projectile;
    [Space]
    public float firerate = 3;
    public bool alternate = true;

    private float lastShot = 0;
    private int index = 0;

    void Start() {
        
    }

    void Update() {
        if (firing && Time.time >= lastShot + (1 / firerate)) {
            if (alternate) {
                Projectile p = Instantiate(projectile, firepoints[index].position, firepoints[index].rotation).GetComponent<Projectile>();
                p.Send(firepoints[index].position, firepoints[index].forward);
                index++;
                if (index >= firepoints.Length) index = 0;
            }
            else {
                for (int i = 0; i < firepoints.Length; i++) {
                    Projectile p = Instantiate(projectile, firepoints[i].position, firepoints[i].rotation).GetComponent<Projectile>();
                    p.Send(firepoints[i].position, firepoints[i].forward);
                }
            }
            lastShot = Time.time;
        }
    }
}
