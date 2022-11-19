using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetCollision : MonoBehaviour
{
    public LayerMask crashLayer;
    [Space]
    public GameObject explosionEffect;
    public GameObject smokeEffect;

    private AIJetMachine pawn;

    private void Start() {
        pawn = GetComponent<AIJetMachine>();
    }

    public void OnCollisionEnter(Collision col) {
        int layer = col.gameObject.layer;
        if (crashLayer == (crashLayer | (1 << layer))) {
            ContactPoint cp = col.GetContact(0);

            pawn.rb.useGravity = false;
            pawn.rb.isKinematic = true;
            pawn.transform.position += (cp.point - transform.position).normalized * 5;

            Instantiate(explosionEffect, pawn.transform.position, Quaternion.LookRotation(cp.point - transform.position));
            Instantiate(smokeEffect, pawn.transform.position, Quaternion.LookRotation(cp.point - transform.position));
        }
        else if (!pawn.isCrashing) pawn.state.Next(new AIJetCrash(pawn));
    }
}
