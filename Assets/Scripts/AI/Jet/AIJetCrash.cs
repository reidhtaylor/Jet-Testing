using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetCrash : AIJetState
{
    public AIJetCrash(AIJetMachine pawn) : base(pawn) {

    }
    
    protected override void OnEnter() {
        pawn.rb.useGravity = true;
        pawn.rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        pawn.rb.AddTorque(Random.insideUnitSphere * 10000, ForceMode.Impulse);
    }
    protected override void OnUpdate() {

    }
    protected override void OnExit() {
    }
}
