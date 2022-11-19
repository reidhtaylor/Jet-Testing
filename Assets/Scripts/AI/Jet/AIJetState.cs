using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetState
{
    public AIJetState(AIJetMachine _pawn) {
        pawn = _pawn;
    }
    public void FirstState() => OnEnter();

    protected AIJetMachine pawn;

    protected virtual void OnEnter() {

    }
    protected virtual void OnUpdate() {
        
    }
    protected virtual void OnExit() {
        
    }

    public void Process() {
        OnUpdate();
    }

    public void Next(AIJetState s) {
        if (pawn.state != null) pawn.state.OnExit();
        pawn.state = s;
        if (s != null) s.OnEnter();
    }
}
