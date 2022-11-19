using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetCollision : MonoBehaviour
{
    public PlayerCameraShake shaker;
    [Space]
    public float shakePower = 2;
    public float shakeLength = 3;
    [Space]
    public LayerMask crashLayer;
    public Collider crashCol;

    private PlayerController player;

    void Start() {
        player = GetComponent<PlayerController>();
    }

    public void OnCollisionEnter(Collision col) {
        ContactPoint cp = col.GetContact(0);
        if (crashLayer == (crashLayer | (1 << col.gameObject.layer)) && cp.thisCollider == crashCol) {
            // Crash
            player.Die();
        }
        else {
            shaker.Shake(shakePower, 1, shakeLength);
        }
    }
}
