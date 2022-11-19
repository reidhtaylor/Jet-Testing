using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetPursueTarget : AIJetState
{
    public AIJetPursueTarget(AIJetMachine pawn, Target t) : base(pawn) {
        target = t;
    }

    private Target target;
    
    protected override void OnEnter() {
    }
    protected override void OnUpdate() {
        float speed = pawn.pursueSpeed;
        
        pawn.rb.velocity = pawn.transform.forward * speed;

        pawn.rb.angularVelocity = Vector3.zero;
    }
    protected override void OnExit() {
        
    }
}
