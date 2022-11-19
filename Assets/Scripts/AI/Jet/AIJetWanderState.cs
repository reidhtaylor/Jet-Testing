using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetWanderState : AIJetState
{
    public AIJetWanderState(AIJetMachine pawn) : base(pawn) {

    }

    float speed = 0;
    float xAngVel = 0;
    float yAngVel = 0;
    
    protected override void OnEnter() {
        pawn.onTargetAdded += (t) => {
            pawn.state.Next(new AIJetPursueTarget(pawn, t));
        };
    }
    protected override void OnUpdate() {
        speed = pawn.glideSpeed;

        if (!MapBounds.InBounds(pawn.transform.position)) {
            if (pawn.transform.position.y <= MapBounds.GetBottom()) {
                float td = Vector3.Angle(pawn.transform.forward, MapBounds.ToBounds(pawn.transform.position));
                xAngVel = td * Time.deltaTime * pawn.verticalCorrectionSpeed;
            }
            else if (pawn.transform.position.y >= MapBounds.GetTop()) {
                float td = Vector3.Angle(pawn.transform.forward, MapBounds.ToBounds(pawn.transform.position));
                xAngVel = td * Time.deltaTime * pawn.verticalCorrectionSpeed;
            }
            else {
                float td = Vector3.Angle(pawn.transform.forward, MapBounds.ToBounds(pawn.transform.position));
                yAngVel = td * Time.deltaTime * pawn.verticalCorrectionSpeed;
            }
        }
        else {
            xAngVel = Mathf.PerlinNoise(Time.time * pawn.wanderVerticalSpeed, 0) * 2 - 1;
            yAngVel = Mathf.PerlinNoise(0, Time.time * pawn.wanderHorizontalSpeed) * 2 - 1;

            EyeCatcher.ViewCatch view = pawn.verticalCatcher.Process();
            if (view != null) xAngVel = view.value * pawn.avoidVerticalSpeed;

            view = pawn.horizontalCatcher.Process();
            if (view != null) yAngVel = view.value * pawn.avoidHorizontalSpeed;
        }

        pawn.rb.velocity = pawn.transform.forward * speed;
        pawn.rb.angularVelocity = (pawn.transform.right * xAngVel * (pawn.wanderVerticalSpeed * pawn.wanderVerticalSpeed)) + (pawn.transform.up * yAngVel * (pawn.wanderHorizontalStrength * pawn.wanderHorizontalSpeed));
    }
    protected override void OnExit() {
        
    }
}
